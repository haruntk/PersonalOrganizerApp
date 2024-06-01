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
        public void SaveState(ProfileMemento memento)
        {
            _undoStack.Push(memento);
            _redoStack.Clear();
        }

        public ProfileMemento Undo()
        {
            if (_undoStack.Count > 1)
            {
                var memento = _undoStack.Pop();
                _redoStack.Push(memento);
                return _undoStack.Peek();
            }
            return null;
        }

        public ProfileMemento Redo()
        {
            if (_redoStack.Count > 0)
            {
                var memento = _redoStack.Pop();
                _undoStack.Push(memento);
                return memento;
            }
            return null;
        }
    }
}
