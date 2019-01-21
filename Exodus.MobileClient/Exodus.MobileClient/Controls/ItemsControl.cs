using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace Exodus.MobileClient.Controls
{
    public class ItemsControl : ContentView
    {
        protected Layout<View> _itemsLayout;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ItemsControl), propertyChanged: (s, n, o) => ((ItemsControl)s).OnItemsSourcePropertyChanged(n,o));

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsControl), propertyChanged: (s, n, o) => ((ItemsControl)s).OnItemTemplatePropertyChanged());

        public static readonly BindableProperty ItemsLayoutProperty =
            BindableProperty.Create(nameof(ItemsLayout), typeof(DataTemplate), typeof(ItemsControl), propertyChanged: (s, n, o) => ((ItemsControl)s).OnItemsLayoutPropertyChanged());

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public DataTemplate ItemsLayout
        {
            get => (DataTemplate)GetValue(ItemsLayoutProperty);
            set => SetValue(ItemsLayoutProperty, value);
        }

        protected virtual View CreateItem(object item)
        {
            if (ItemTemplate == null)
            {
                return new Label() { Text = item.ToString() };
            }
            else
            {
                var itemView = (View)ItemTemplate.CreateContent();
                itemView.BindingContext = item;
                return itemView;
            }
        }

        protected virtual void CreateItemsLayout()
        {
            Content = ItemsLayout != null ?
                (Layout)ItemsLayout.CreateContent() :
                new AbsoluteLayout();

            if (Content is Layout<View> viewLayout)
            {
                _itemsLayout = viewLayout;
            }
            else
            {
                _itemsLayout = FindItemsHost(Content);
            }
        }

        void RejiggerItems()
        {
            if (_itemsLayout == null)
            {
                CreateItemsLayout();
            }

            _itemsLayout.Children.Clear();

            if (ItemsSource != null)
            {
                foreach (object item in ItemsSource)
                {
                    _itemsLayout.Children.Add(CreateItem(item));
                }
            }
        }
        void OnItemsSourcePropertyChanged(object oldValue, object newValue)
        {
            if(oldValue != newValue)
            {
                var oldCollectionNotifyChangeable = oldValue as INotifyCollectionChanged;
                var newCollectionNotifyChangeable = newValue as INotifyCollectionChanged;

                if(oldValue != null)
                    oldCollectionNotifyChangeable.CollectionChanged -= CollectionNotifyChangeable_CollectionChanged;

                if(newValue != null)
                    newCollectionNotifyChangeable.CollectionChanged += CollectionNotifyChangeable_CollectionChanged;
            }

            this.RejiggerItems();
        }

        void CollectionNotifyChangeable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {    
                foreach (object item in e.NewItems)
                {
                    _itemsLayout.Children.Add(CreateItem(item));
                }
            }

            if(e.OldItems != null)
            {
                foreach (object item in e.OldItems)
                {
                    var potentialMatchingViewItem = this.GetViewItemFromDataItem(item);
                    if (potentialMatchingViewItem != null && this._itemsLayout.Children.Contains(potentialMatchingViewItem))
                        _itemsLayout.Children.Remove(potentialMatchingViewItem);
                }
            }
        }

        View GetViewItemFromDataItem(object dataItem)
        {
            View matchingViewItem = null;
            foreach(View viewItem in _itemsLayout.Children)
            {
                if (viewItem.BindingContext == dataItem)
                {
                    matchingViewItem = viewItem;
                    break;
                }
            }

            return matchingViewItem;
        }

        void OnItemTemplatePropertyChanged()
        {
            if (_itemsLayout == null)
            {
                return;
            }

            this.RejiggerItems();
        }

        void OnItemsLayoutPropertyChanged()
        {
            CreateItemsLayout();

            this.RejiggerItems();
        }

        Layout<View> FindItemsHost(View currView)
        {
            if (currView is Layout<View> viewLayout && LayoutEx.GetIsItemsHost(viewLayout))
            {
                return viewLayout;
            }
            else
            {
                if (currView is Layout layoutView)
                {
                    foreach (Element e in layoutView.Children)
                    {
                        Layout<View> itemsHost = FindItemsHost((View)e);
                        if (itemsHost != null)
                        {
                            return itemsHost;
                        }
                    }
                }

                return null;
            }
        }
    }

    static class LayoutEx
    {
        public static readonly BindableProperty IsItemsHostProperty =
            BindableProperty.CreateAttached("IsItemsHost", typeof(bool), typeof(Layout<View>), false);

        public static bool GetIsItemsHost(Layout<View> layout)
        {
            return (bool)layout.GetValue(IsItemsHostProperty);
        }

        public static void SetIsItemsHost(Layout<View> layout, bool value)
        {
            layout.SetValue(IsItemsHostProperty, value);
        }
    }
}
