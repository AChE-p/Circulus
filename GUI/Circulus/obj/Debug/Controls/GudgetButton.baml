   M S B A M L   `   `   `  ����C  ?Circulus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CirculusCirculus  5   	       Circulus.GudgetButton   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   (localclr-namespace:Circulus     5   	   ���#7EC2FF  J� Opacity$	  0.5Q�5   /   $��0q�6=   $��60��6Q   $��60��6]    � 	Resources 5       	  FIcon �� 
TargetType �� Property �� Value  DefaultGudgetButton   Circulus.GudgetButton �� 
TargetType ��Name �� RelativeSource �� Mode	 �� Path
 �� CornerRadius �� Orientation ��Name J� RenderTransformOrigin �� Angle �� Triggers P� Property P� Value �� 
TargetName ]� 
Conditions �� Property �� Value N� EnterActions �� 
TargetName �� TargetProperty	 W� To g� Duration N� ExitActions  TransparentButton �� BasedOn��%�  &	        &	 J     7   &	 �
    7  �� 5	   
   " ��6   .�5
      ��  � ��
 ���� 6+   5      �� 5   
   "  6,   .�5      ��  � �� 5      �� 5      Button_Template��"  6?   .R�5      �� $ border��6!   ��61   ��  �� $ TemplatedParent=�$	 
Background���5   %   ��  �� $ TemplatedParent=�$	 Height�
 5   %   ��  �� $ TemplatedParent=�$	 CornerRadius���5   %   ��  �� $ TemplatedParent=�$	 Width�._�5      �� $ 
Horizontal=�6)   $��Center=�6B   #��z��5      #��z��5      .+�5   "   �� $
 icon��6,   $ 0.5,0.5&�5      8��  ��5   ;   ��  �� $ TemplatedParent=�$	 FIconMargin���5      ��  �� $ TemplatedParent=�$	 FIcon���5      ��  �� $ TemplatedParent=�$	 	FIconSize���5      ��  �� $ TemplatedParent=�$	 
Foreground�z�5   &   �� 5   *   	transIcon��$ 0Q�6M   5    #   �� 5"   "   $	 txt��6H   $��Center=�6,   ��5#      ��  �� $ TemplatedParent=�$	 ContentDecorations���5$   0   ��  �� $ TemplatedParent=�$	 Content���5%   0   ��  �� $ TemplatedParent=�$	 FontSize���5&   0   ��  �� $ TemplatedParent=�$	 
Foreground�6�   5'      5(       5*      P� 5,       �@J�IsMouseOver . . �5-   "   �� $ border��5.   <    5-   ?   ��  �� $ TemplatedParent=�$	 MouseOverBackground� � ��5.   <   �� 5/   "   $
 icon��50   <    5/   ?   ��  �� $ TemplatedParent=�$	 MouseOverForeground� � ��50   <   �� 51   "   $	 txt��52   <    51   ?   ��  �� $ TemplatedParent=�$	 MouseOverForeground� � ��52   <   53      ]� 55       56   "   �� 57   &    �@J�IsMouseOver . 6V   �� 58   &    �@ AllowsAnimation . 6Z    5:   "   �� 5;   &   .��5<   *   �� .��5=   .   W� $ 	transIcon��6>   $ Angle�6`   $	 180X�6�   $ 0:0:0.2D�6�   5>   +   5?   '    5A   "   �� 5B   &   .��5C   *   �� .��5D   .   W� $ 	transIcon��6>   $ Angle�6`   $ 0X�6�   $ 0:0:0.2D�6�   5E   +   5F   '   5H      P� 5J       � �� . . �5K   "   �� $ border��5L   :    5K   ?   ��  �� $ TemplatedParent=�$	 PressedBackground� � ��5L   :   �� 5M   "   $
 icon��5N   :    5M   ?   ��  �� $ TemplatedParent=�$	 PressedForeground� � ��5N   :   �� 5O   "   $	 txt��5P   :    5O   ?   ��  �� $ TemplatedParent=�$	 PressedForeground� � ��5P   :   5Q      P� 5R       � {� .  . �5S   "   �� $ border��6H    �@J�Opacity$	 0.5Q�6<   6H   5T      5V      5X      5Y      �� 5Z   
   "  6*   8   .�5[      ��  � ��
 ���� 6+   �� 5\       �@ MouseOverBackground
 ���� 64   �� 5]       �@ PressedBackground
 ���� 62   �� 5^       � ��
 �����6+   �� 5_       �@ MouseOverForeground 64   �� �� 5`       �@ PressedForeground 62   �� �� 5a       � ��$ Center=�6;   �� 5b       � ��$
 Auto��6'   �� 5c       � ��$
 Auto��6&   �� 5d       �@ CornerRadius$ 0��6-   �� 5e       � ��$ 13(�6)   �� 5f       �@ 	FIconSize$ 20��6*   �� 5g       � ��$ 3,1,3,1q�6(   �� 5h       � ��$  ��6(   �� 5i       �@ FIconMargin$ 0,0,2,0q�6,   �� 5j       �@ AllowsAnimation .  60   �� 5k       �@�Cursor$
 Hand��6'   5l      5m      .��5o      � ��6   �� ��6   �� .+�5p   
        FIcon$	 ❐��6   $��1,-5,1,1q�6.      FIconMargin$ 0q�6@   $��2(�6P      	FIconSize$ 50��6]   #���  6l   6�   5r      5t      