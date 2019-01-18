using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ePs.PatientLive.Framework.Utilities
{
    /// <summary>
    /// Utility class for implementing incremental loading. Just subclass, and implement
    /// LoadPageItemsAsync (don't forget to use the async keyword!).
    /// </summary>
    /// <typeparam name="T">Type of item in collection</typeparam>
    public abstract class IncrementalLoadingCollection<T>
        : ExtendedObservableCollection<T>, ISupportIncrementalLoading
    {
        public event EventHandler<IncrementalLoadingStartedEventArgs> LoadingStarted;
        public event EventHandler<IncrementalLoadingCompletedEventArgs> LoadingCompleted;

        #region Fields

        private uint _currentPageLowerBound = 0;
        private uint _currentPageUpperBound = 0;
        private uint _skip = 0;
        private uint _take = 0;
        private uint _totalItemsCount = 0;

        private bool _hasMoreItems = true;

        /// <summary>
        /// Gets/sets whether the collection should continue loading data from source.        
        /// </summary>            
        public bool HasMoreItems
        {
            get { return _hasMoreItems; }
            private set { _hasMoreItems = value; }
        }

        #endregion

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            CoreDispatcher dispatcher = Window.Current.Dispatcher;

            var results = Task.Run(() => LoadItems(count, dispatcher));
            return results.AsAsyncOperation<LoadMoreItemsResult>();
        }

        private async Task<LoadMoreItemsResult> LoadItems(uint count, CoreDispatcher dispatcher)
        {
            uint resultItemsCount = 0;
            uint totalItemsCount = _totalItemsCount;

            // Calculate current page bounds.
            _skip = Convert.ToUInt32(this.Count);
            _take = count;
            _currentPageLowerBound = _currentPageUpperBound + 1;
            _currentPageUpperBound = _currentPageLowerBound + count - 1;

            if(HasMoreItems)
            {
                if (LoadingStarted != null)
                {
                    // We have to use the Dispatcher to marshal the context of the thread because event could me managed in a different thread.
                    // Omitting the use of RunAsync raises a cross thread violation exception.                    
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        LoadingStarted(this, new IncrementalLoadingStartedEventArgs(count, _currentPageLowerBound, _currentPageUpperBound));
                    });
                }

                try
                {
                    // Load items.                        
                    IncrementalLoadingResult<T> result = await LoadPageItemsAsync(_skip, _take);

                    if (result != null)
                    {
                        if (result.Items != null && result.Items.Count() > 0)
                        {
                            resultItemsCount = (uint)result.Items.Count();
                            totalItemsCount = result.TotalItemsCount;

                            // When we add the items to the collection we need to be careful because we generate them in a separate thread 
                            // so we have to use the Dispatcher to marshal the context of the thread because the collection is binded to a UI element.
                            // Omitting the use of RunAsync raises a cross thread violation exception.
                            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                foreach (var i in result.Items)
                                { 
                                    if(!this.Contains(i))
                                        this.Add(i);
                                }                               
                            });
                        }

                        if (this.Count >= totalItemsCount)
                        {
                            HasMoreItems = false;
                        }
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }
                catch
                {
                    HasMoreItems = false;
                    totalItemsCount = 0;
                }

                _totalItemsCount = totalItemsCount;

                // We have to use the Dispatcher to marshal the context of the thread because properties could be binded to a UI element.
                // Omitting the use of RunAsync raises a cross thread violation exception.                    
                if (LoadingCompleted != null)
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        LoadingCompleted(this, new IncrementalLoadingCompletedEventArgs(resultItemsCount, _totalItemsCount));
                    });
                }
            }

            return new LoadMoreItemsResult()
            {
                Count = resultItemsCount
            };

        }

        /// <summary>        
        /// LoadPageItemsAsync is where you actually get items from a data source (web service, database, XML, etc.)
        /// When implementing this method, make sure you make it async.
        /// </summary>
        /// <param name="pageLowerBound">Current page lower bound.</param>
        /// <param name="pageUpperBound">Current page upper bound.</param>
        /// <returns></returns>
        protected abstract Task<IncrementalLoadingResult<T>> LoadPageItemsAsync(uint pageLowerBound, uint pageUpperBound);
    }
}
