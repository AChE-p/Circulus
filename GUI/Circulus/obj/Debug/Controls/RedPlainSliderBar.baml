   M S B A M L   `   `   `  ����C  ?Circulus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CirculusCirculus  5           Circulus.RedPlainSliderBar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Circulus     5        � 	Resources  5	         SliderBackgroundBrush �� 
StartPoint �� EndPoint � Color � Offset  SliderThumbBrush  SliderTextBrush  SliderRepeatButton �� 
TargetType �� Property �� Value �� 
TargetType  SliderRepeatButtonT  SliderThumb	 � ColumnDefinitions
 �� CornerRadius  SliderStyle J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth �� RelativeSource �� Path K� ZIndex �� ElementName �� StringFormat � DataContext ]� DecreaseRepeatButton ]� IncreaseRepeatButton ]� Thumb��%z  &	        &	 �     &	 ,    &	 �    7   &	 n    &	 "    7   7   &	 �    7  7  7  �� 5
   
   $	 0,0&�6<   $	 0,1&�6M   .��5      � $ #FB9289��6   $ 0Q�6+   � 5      $ #FA7267��6   $	 0.6Q�6+   � 5      $ #FD4B4B��6   $ 1Q�6+   5      �� 5   
   $	 0,0&�67   $	 0,1&�6H   .��5      � $ 	#FFD9D3E8��6   $ 0Q�6-   5      �� 5   
   $	 0,0&�66   $	 0,1&�6G   .��5      � $ #7CCE45��6   $ 0Q�6+   � 5      $ #4EA017��6   $ 1Q�6+   5      �� 5   
   " ��6+   .�5      ��  � |� .  6*   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� 8��  6!   5      5      5      �� 5    
   " ��6,   .�5!      ��  � |� .  6*   �� 5"       � �� 5#      �� 5$      " ��6&   .R�5%      �� 
������ 6!   5&      5(      5)      �� 5*   
   " n�6$   .�5+      ��  � |� .  6*   �� 5,       � �� 5-      �� 5.      " n�6&   .R�5/      � 	 50      �� 51   "   �� 52   "   .+�54      �� 8��  6%   �� 55      $��2��6%   $
 2��69   8�� $��15��6~   ._�56   "   �� $	��❱��6,   ���#C62427$��Center=�6Q   $��Center=�6n   57      58      59      5;      5<      �� 5=   
   " ��6$   .�5>      ��  � |� .  6*   �� 5?       � �� 5@      �� 5A      " ��6&   .R�5B      �  5C        5D   "   $
 1.75Q�63   $	 0.1Q�6E   .+�5F      �� 
�������$��Center=�6�   $��Center=�6�   $��14(�6�   ��6(   ��  �� *TemplatedParent+$	 Tag�6�   �� 5G      $��2��6(   $ 3��6�   ���#BA36F7$��Center=�6�   $��Center=�6�   $��14(�6�   $��Noto Sans CJk Medium)�6�   ��68   �� $ 
PART_Track��$ Value�$ {0:N2}�� 6  �� �� 5H      $��1��6%   
�������$
��0.25q�6H   $
 0,2,2,0��6_   ._�5I   "   ]� $��
PART_Track��68   $��1��6(    5J   &   �� 5K   *   8��  $��Slider.DecreaseLarge��6c    5M   &   �� 5N   *   8�� $��Slider.IncreaseLarge��6d    5P   &   n� 5Q   *   8�� 60   5S   #   5T      5U      5V      5X      5Y      5Z      .��5[      � .+�5\   
   �� -   5         ��Name$ slider��5\      #���  6!   $
��Left=�6G   $	��Top=�6b   $	��300��6z   $��18��6�    � SmallChange$ 0Q�6�    � Minimum$
 0.01Q�6�   5]      5^      