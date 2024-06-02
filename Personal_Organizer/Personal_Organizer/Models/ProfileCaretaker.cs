using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class ProfileCaretaker // Geri dönüş adımlarını tutacak sınıf
    {
        private readonly Stack<ProfileMemento> _undoStack = new Stack<ProfileMemento>();
        private readonly Stack<ProfileMemento> _redoStack = new Stack<ProfileMemento>();
        public void SaveState(User user)
        {
            _undoStack.Push(user.CreateMemento());
        }

        public ProfileMemento Undo(User user)
        {
            if (_undoStack.Count > 1)
            {
                _redoStack.Push(_undoStack.Pop());
                var memento = _undoStack.First();
                user.RestoreMemento(memento);
            }
            return null;
        }

        public ProfileMemento Redo(User user)
        {
            if (_redoStack.Count > 0)
            {
                _undoStack.Push(_redoStack.First());
                var memento = _redoStack.Pop();
                user.RestoreMemento(memento);
            }
            return null;
        }
    }
}
