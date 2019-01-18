using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class ObservableObject<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(Expression<Func<T, object>> property)
        {
            if (property == null || property.Body == null) return;

            var memberExp = property.Body as MemberExpression;
            if (memberExp == null)
            {
                UnaryExpression unary = property.Body as UnaryExpression;
                if (unary != null) memberExp = unary.Operand as MemberExpression;
                if (memberExp == null) return;
            }

            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(memberExp.Member.Name));
        }
    }
}
