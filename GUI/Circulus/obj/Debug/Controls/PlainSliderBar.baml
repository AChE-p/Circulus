   M S B A M L   `   `   `  ����C  ?Circulus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CirculusCirculus  5          Circulus.PlainSliderBar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Circulus     5        � 	Resources  5	         SliderBackgroundBrush �� 
StartPoint �� EndPoint � Color � Offset  SliderThumbBrush  SliderTextBrush  SliderRepeatButton �� 
TargetType �� Property �� Value �� 
TargetType  SliderRepeatButtonT  SliderThumb	 � ColumnDefinitions
 �� CornerRadius  SliderStyle J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth �� RelativeSource �� Path K� ZIndex �� ElementName �� StringFormat � DataContext ]� DecreaseRepeatButton ]� IncreaseRepeatButton ]� Thumb��%J  &	        &	 �     &	 �     &	 �    7   &	 >    &	 �    7   7   &	 z    7  7  7  �� 5
   
   $	 0,0&�6<   $	 0,1&�6M   .��5      � $ #FFDA55��6   $ 0Q�6+   � 5      $ #FFDA55��6   $ 1Q�6+   5      �� 5   
   $	 0,0&�67   $	 0,1&�6H   .��5      � $ 	#FFD9D3E8��6   $ 0Q�6-   5      �� 5   
   $	 0,0&�66   $	 0,1&�6G   .��5      � $ #7CCE45��6   $ 0Q�6+   � 5      $ #4EA017��6   $ 1Q�6+   5      �� 5   
   " ��6+   .�5      ��  � |� .  6*   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� 8��  6!   5      5      5      �� 5   
   " ��6,   .�5       ��  � |� .  6*   �� 5!       � �� 5"      �� 5#      " ��6&   .R�5$      �� 
������ 6!   5%      5'      5(      �� 5)   
   " n�6$   .�5*      ��  � |� .  6*   �� 5+       � �� 5,      �� 5-      " n�6&   .R�5.      � 	 5/      �� 50   "   �� 51   "   .+�53      �� 8��  6%   �� 54      $��2��6%   $
 2��69   8�� $��15��6~   ._�55   "   �� $	��❱��6,   ���#F7B705$��Center=�6Q   $��Center=�6n   56      57      58      5:      5;      �� 5<   
   " ��6$   .�5=      ��  � |� .  6*   �� 5>       � �� 5?      �� 5@      " ��6&   .R�5A      �  5B        5C   "   $
 1.75Q�63   $	 0.1Q�6E   .+�5E      �� 
�������$��Center=�6�   $��Center=�6�   $��14(�6�   ��6(   ��  �� *TemplatedParent+$	 Tag�6�   �� 5F      $��2��6(   $ 3��6�   ���#F6776D$��Center=�6�   $��Center=�6�   $��14(�6�   $��Noto Sans CJk Medium)�6�   ��68   �� $ 
PART_Track��$ Value�$ {0:N2}�� 6  �� �� 5G      $��1��6%   
�������$
��0.25q�6H   $
 0,2,2,0��6_   ._�5H   "   ]� $��
PART_Track��68   $��1��6(    5I   &   �� 5J   *   8��  $��Slider.DecreaseLarge��6c    5L   &   �� 5M   *   8�� $��Slider.IncreaseLarge��6d    5O   &   n� 5P   *   8�� 60   5R   #   5S      5T      5U      5W      5X      5Y      .��5Z      � .+�5[   
   �� -   5         ��Name$ slider��5[      #���  6!   $
��Left=�6G   $	��Top=�6b   $	��300��6z   $��18��6�    � SmallChange$ 0Q�6�    � Minimum$
 0.01Q�6�   5\      5]      