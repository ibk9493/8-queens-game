using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyAttemptAtQueens
{
    class QueenSpot
    {

        public event PropertyChangedEventHandler PropertyChanged;

      //  public string Color { get; }
        public int X { get; }
        public int Y { get; }
        private Button spot;
        private bool _hasQueen;
        private bool _accessible;
        private bool _accessibleView;

        public bool HasQueen
        {
            get
            {
                return _hasQueen;
            }
            set
            {
                if (_hasQueen == value)
                    return;

                _hasQueen = value;
                OnPropertyChanged();
            }
        }
        public bool Accessible
        {
            get
            {
                return _accessible;
            }
            set
            {
                if (_accessible == value)
                    return;

                _accessible = value;
                AccessibleView = !value;
                OnPropertyChanged();
            }
        }
        public bool AccessibleView
        {
            get
            {
                return _accessibleView;
            }
            private set
            {
                if (_accessibleView == value)
                    return;

                _accessibleView = value;
                OnPropertyChanged();
            }
        }

        public Button Spot
        {
            get
            {
                return spot;
            }

            set
            {  if (spot == value)
                    return;
                spot = value;
                OnPropertyChanged();
            }
        }

        public QueenSpot(int x, int y,Button btn)
        {
            //Color = color;
            X = x;
            Y = y;
            Spot = btn;
            HasQueen = false;
            Accessible = true;
        }

        public void Reset()
        {
            HasQueen = false;
            Accessible = true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
