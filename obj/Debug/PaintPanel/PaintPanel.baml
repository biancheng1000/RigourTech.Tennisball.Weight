   M S B A M L   `   `   `  ����W  SRigourTech.Tennisball.Weight, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:InkCanvasTestInkCanvasTest  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35`Dclr-namespace:System.Windows.Controls;assembly=PresentationFrameworkSystem.Windows.Controls 5      -    'RigourTech.Tennisball.Weight.PaintPanel   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:InkCanvasTest     5      TsysDclr-namespace:System.Windows.Controls;assembly=PresentationFramework     5      $	��400��5      $	��600��6,     � 	Resources  5	         	EditModel S� 
MethodName S� 
ObjectType2  ,System.Windows.Controls.InkCanvasEditingMode S� MethodParameters M� TypeName  DrawType   InkCanvasTest.DrawTypeEum  RadioButtonList �� 
TargetType �� Property �� Value �� 
TargetType	 J� IsHitTestVisible
 �� 
IsSelected d� 	IsChecked��%�  &	        &	 y     &	 �     S� 5
   
   $ 	GetValues��6/   "  5   
    5      M� 5      $ sys:InkCanvasEditingMode��6   5      S� 5   
   $ 	GetValues��6.   "  5   
    5      M� 5      $ local:DrawTypeEum��6   5      �� 5   
   " ��6(   .�5      ��  � �� 6,   Z� �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      " ��6   .�5      ��  � ��$ 2q�63   �� 5       � �� 5      �� 5   "   " ��62   .R�5    &   �� 
������ ._�5"   *   	� |�.  	 .  # z
 5&   ,   .��5(   .   �� 5*   +   5,   '   5.   #   52      54      58      59      5:      .��5;      �  � RowDefinitions 5<   
   �� 5=      �� 5>      $��50 �6    � ColumnDefinitions 5@   
   �� 5A      �� 5B      $	��180 �6   .+�5D   
   #   InkCanvasTest.CustomInkCanvas  -   5          Name$
 test��5D   {    �� 
Background
 ����  �� EditingMode 6    ��  �� Path$ SelectedItem� �� ElementName$ 
editSource��   DrawType 6�   �� $ SelectedItem�$ 
DrawSource�� �� DefaultDrawingAttributes 5F      *  $System.Windows.Ink.DrawingAttributes  5G         Color$	 Red��6$      Width$ 2Q�60      Height$ 2Q�6:   5I      �� 5K   
   $��形状��6   $��1��6   $��2��6,   .��5L      �� -   5         ��Name$ 
DrawSource��5L      #��[ 6c    �� SelectedIndex$ 0��6�   ��6*   ��  �� Source# [ ��5M      �� 5N      .R�5O      �  � Orientation$ 
Horizontal=�6$   5P      5R      5S      �� 5U   
   $��操作��6   $��1��6   .��5V      �� -   5        $ 
editSource��5V      #��[ 6d   $ 0��6�   ��6*   �� # [  6�   5W      5X      5Y      