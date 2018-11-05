using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreWpf.DataSubscribtion
{

    public interface IAddFilmObservable
    {
        void Subscribe(IAddFilmObserver observer);
        void Unsubscribe(IAddFilmObserver observer);
    }
}
