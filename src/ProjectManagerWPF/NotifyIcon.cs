using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Application=System.Windows.Application;
using ButtonBase=System.Windows.Controls.Primitives.ButtonBase;
using MouseEventArgs=System.Windows.Forms.MouseEventArgs;

namespace ProjectManagerWPF
{
    /// <summary>
    /// Provides the possibility of registering a notify icon in the system tray and showing balloon tips.    
    /// </summary>
    /// <remarks>
    /// There is no equivalent to the System.Windows.Forms.NotifyIcon in WPF
    /// (see http://msdn.microsoft.com/de-de/library/ms750559.aspx).
    /// </remarks>
    [ContentProperty("Text")]
    [DefaultEvent("MouseDoubleClick")]
    public class NotifyIcon : ButtonBase
    {
        /// <summary>
        /// Identifies the <see cref="Icon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NotifyIcon));

        /// <summary>
        /// Gets or sets the icon which will be displayed in the system tray. This is a dependency property.
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Text"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(NotifyIcon));

        /// <summary>
        /// Gets or sets the text which will be displayed as a tool tip for the system tray icon. This is a dependency property.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private System.Windows.Forms.NotifyIcon notifyIcon;

        private bool initialized;

        private void InitializeNotifyIcon()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Text = Text,
                Icon = FromImageSource(Icon),
                Visible = FromVisibility(Visibility)
            };

            notifyIcon.MouseUp += OnMouseUp;
            notifyIcon.MouseClick += OnMouseClick;

            initialized = true;
        }

        private static Icon FromImageSource(ImageSource icon)
        {
            if (icon != null)
            {
                var iconUri = new Uri(icon.ToString());
                var resourceStream = Application.GetResourceStream(iconUri);
                if (resourceStream != null)
                    return new Icon(resourceStream.Stream);
            }
            return null;
        }

        private static bool FromVisibility(Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            InitializeNotifyIcon();
            Dispatcher.ShutdownStarted += OnDispatcherShutdownStarted;
        }

        private void OnDispatcherShutdownStarted(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            var mbe = CreateMouseButtonEventArgs(e);
            OnRaiseEvent(PreviewMouseUpEvent, mbe);
            if (!mbe.Handled)
            {
                OnRaiseEvent(MouseUpEvent, mbe);
                if (!mbe.Handled && mbe.ChangedButton == MouseButton.Right)
                    ShowContextMenu();
            }
        }

        private static MouseButtonEventArgs CreateMouseButtonEventArgs(MouseEventArgs e)
        {
            return new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, ToMouseButton(e.Button));
        }

        private static MouseButton ToMouseButton(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return MouseButton.Left;
                case MouseButtons.Right:
                    return MouseButton.Right;
                case MouseButtons.Middle:
                    return MouseButton.Middle;
                case MouseButtons.XButton1:
                    return MouseButton.XButton1;
                case MouseButtons.XButton2:
                    return MouseButton.XButton2;
                default:
                    throw new ArgumentException("Unknown value", "button");
            }
        }

        private void OnRaiseEvent(RoutedEvent routedEvent, RoutedEventArgs e)
        {
            e.RoutedEvent = routedEvent;
            RaiseEvent(e);
        }

        private void ShowContextMenu()
        {
            if (ContextMenu != null)
            {
                ContextMenuService.SetPlacement(ContextMenu, PlacementMode.MousePoint);
                ContextMenu.IsOpen = true;
            }
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OnRaiseEvent(ClickEvent, CreateMouseButtonEventArgs(e));
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (initialized)
            {
                switch (e.Property.Name)
                {
                    case "Icon":
                        notifyIcon.Icon = FromImageSource(Icon);
                        break;
                    case "Text":
                        notifyIcon.Text = Text;
                        break;
                    case "Visibility":
                        notifyIcon.Visible = FromVisibility(Visibility);
                        break;
                }
            }
        }
    }
}