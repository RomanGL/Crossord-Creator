using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace CCApp.Behaviors
{
    public class ShowContextMenuBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            if (AssociatedObject != null)
                AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.ContextMenu != null)
                AssociatedObject.ContextMenu.IsOpen = true;
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
                AssociatedObject.Click -= AssociatedObject_Click;
        }
    }
}
