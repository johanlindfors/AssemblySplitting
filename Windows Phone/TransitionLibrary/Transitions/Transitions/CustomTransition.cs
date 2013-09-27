using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TransitionLibrary.Transitions.Transitions
{
    public class CustomTransition : TransitionElement
    {
        ResourceDictionary storyboardsDictionary = null;
        string path = "Assets/Storyboards.xaml";

        public override ITransition GetTransition(System.Windows.UIElement element)
        {
            element.RenderTransform = new CompositeTransform();
            element.RenderTransformOrigin = new Point(0.5, 0.15);

            var storyBoard = LoadStoryboard();
            Storyboard.SetTarget(storyBoard, element);
            return new Transition(element, storyBoard);
        }

        public static readonly DependencyProperty TransitionTypeProperty = DependencyProperty.Register("TransitionType", typeof(PageTransitionType), typeof(CustomTransition), new PropertyMetadata(null));

        public PageTransitionType TransitionType
        {
            get { return (PageTransitionType)GetValue(TransitionTypeProperty); }
            set { SetValue(TransitionTypeProperty, value); }
        }

        private Storyboard LoadStoryboard()
        {
            //return App.Current.Resources["SlideUp"] as Storyboard;

            if (storyboardsDictionary == null)
                CreateStoryboardDictionary();

            if (storyboardsDictionary.Contains(TransitionType.ToString()))
                return storyboardsDictionary[TransitionType.ToString()] as Storyboard;
            else
                return new Storyboard();
        }

        private void CreateStoryboardDictionary()
        {
            var uri = new Uri(path, UriKind.Relative);
            var streamResourceInfo = Application.GetResourceStream(uri);
            using (var reader = new StreamReader(streamResourceInfo.Stream))
            {
                var xamlString = reader.ReadToEnd();
                storyboardsDictionary = XamlReader.Load(xamlString) as ResourceDictionary;
            }
        }
    }
}
