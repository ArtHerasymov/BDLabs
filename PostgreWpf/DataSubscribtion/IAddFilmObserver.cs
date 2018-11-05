using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreWpf.DataSubscribtion
{
    public interface IAddFilmObserver
    {
        void OnFilmAdded(Film film);
        void OnActorAdded(Actor actor);
        void OnDirectorAdded(Director director);
    }

}
