   M S B A M L   `   `   `  ����H  DWPF UI Fucker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WPF_UI_FuckerWPF_UI_Fucker  5   	   U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nulleHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf 5   	       WPF_UI_Fucker.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WPF_UI_Fucker     5   	   WmdHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf     5   	   -   5          � Title$  
MainWindow��5	   	   $	��355��6   $��528.5��6)    � AllowsTransparency .  � 
ResizeMode$ NoResize=�6Q    � WindowStyle$
 None=�6g   ��6z   Z�  � 	Resources 5
         Startup �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%~  &	        �� 5   
   .��5      U� $ (UIElement.Opacity)�6,   $
 grid��6\   .\�5        $ 0��6'   $ 0Q�63     5      $ 0:0:0.4��6'   $ 1Q�69   5      U� 5      $3 -(UIElement.Effect).(DropShadowEffect.Opacity)�6,   $
 grid��6v   .\�5        $ 0��6'   $ 0Q�63     5      $ 0:0:0.4��6'   $	 0.6Q�69   5      5      5      	 � Triggers	 5      :� 5   
   
 :� RoutedEvent$
 FrameworkElement.Loaded��6   .[�5      �� #��[  6   5      .��5      � -   5         �Name$
 grid��5      $��10q�6   
������� J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         	Direction$	 270Q�6      Opacity$	 0.6Q�6/    � ColumnDefinitions 5   
   �� 5       .+�5"   
   � -   5         �Name$
 prog��5"      $��5.333��6$   $��	0,147,0,0q�63   $	��Top=�6F   ��6^   Z� ��6t   Z� �� 5#   
   -   5         ��Name$ 
textBlock2��5#       �� TextAlignment$ Center=�6(   $��139.225,0,132.923,113.756q�6?   $
��Wrap=�6b   $��18(�6v   $��27��6�   $��Bottom=�6�   $��Yu Gothic UI Light)�6�   $��236.352��6�   $��Center=�6�   �� 5%   
   $	��147��6   $	��Top=�6!   $��0��69    J� OpacityMask
 �   ���5&      �� 5'       �� ImageSource$ 60294685_p0.png��6    j� Stretch$ UniformToFill=�6;   5)      �� 5*   
   $	��147��6   $��0��6!   $	��Top=�65   ���	#4C0000006M   �� 5,   
   -   5        $ 	textBlock��5,      $��36��6'   $��20,7.244,0,0q�63   $
��Wrap=�6I   $��Pixiv ��6]   $	��Top=�6k   
�������$��Yu Gothic UI Light)�6�   $��18(�6�   $
��Left=�6�   $��130.5��6�   $��Light"�6�      WPF_UI_Fucker.CloseButton  5-   
   -   5          Name$ closebtn��5-      $��Right=�6.   $��16.667��6J   $��	0,13,21,0q�6Z   $	��Top=�6m   $��16.667��6�   A� 5.   
   ���	#FFFFF643$��Right=�6#   $��16.667��6?   $��0,13,54.833,0q�6O   $	��Top=�6f   $��16.667��6~   A� 5/   
   -   5         A�Name$
 avat��5/      $��209,80.244,207.5,162.756q�6    $��6��6B   ��50      �� 51      $ 
avatar.png��6   53      '  !MaterialDesignThemes.Wpf.PackIcon  54   
   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind$ Account=�6   $��24��6%   $��24��61   $
��Left=�6<   $��Bottom=�6W   $��0,0,1,0q�6r   $��90.225,0,0,115.756q�6�   �� 55   
   -   5         ��Name$ textBox_Copy��55   B     MaterialDesignTextBox#���  6   $��18(�6X   $ Center=�6r   $��
UltraLight"�6�   $��134.414,0,132.923,111.756q�6�    �� TextWrapping$
 Wrap=�6�   $��0,0,0,1q�6�   $��29��6	  $��Bottom=�6   J� RenderTransformOrigin$ 0.526,-0.527&�60  ���	#000000006r  G� 56   
   -	   5         G�Name$ Passwordbox��56   J     MaterialDesignPasswordBox#���  6   $��134.414,0,132.923,55q�6_   $ Center=�6}   $��26��6�   $��Bottom=�6�    G� SelectionOpacity$ 0Q�6�   6�   �� 57   
   -
   5         ��Name$ button1��57        MaterialDesignToolButton#���  6"   $��Right=�6U   $��44.756��6q   $��0,0,41.24,90q�6�   $��Bottom=�6�   $��55.304��6�    �� 	IsDefault . .��58        $ 
ArrowRight=�6   $��24��6,   $��24��68   $��Stretch=�6C   $��Stretch=�6a   $��0,0,1,0q�6}   59      �� 5:   
   -   5        $ button��5:       , (MaterialDesignFloatingActionAccentButton#���  6!   $��Right=�6d   $
��Auto��6�   $��0,124.244,41.24,162.756q�6�   $��48��6�   ���	#DDFFFFFF.��5;        $ Settings=�6   $��24��6*   $��24��66   $��Center=�6A   $��Center=�6^   $��0,0,1,0q�6y   5<        5=   
   $
 Lock=�6   $��20��6"   $��20��6.   $
��Left=�69   $��Bottom=�6T   $��0,0,1,0q�6o   $��92.226,0,0,55q�6�   �� 5>   
   -   5        $ 	ErrorCode��5>      $ Center=�6'   $��24��6>   $��133.225,0,133.5,10q�6J   $
��Wrap=�6f   $��	ErrorCode��6z   $��Bottom=�6�   
���  ��6�   �� 5?   
   -   5        $ button2��5?      $��忘记密码��6"     MaterialDesignFlatButton#���  61   $��Right=�6d   $��26��6�   $��0,0,24.334,55q�6�   $��Bottom=�6�   $��91.877��6�   6�   �� 5@   
   -   5        $ 
textBlock1��5@      $
��Left=�6(   $��14.244��6C   $��139.225,0,0,143.756q�6S   $
��Wrap=�6p   $��	用户名��6�   $��Bottom=�6�   $��66.775��6�   $��10(�6�   �� 5A   
   -   5        $ textBlock1_Copy��5A      $
��Left=�6-   $��14.244��6H   $��138.225,0,0,86.913q�6X   $
��Wrap=�6t   $��密码��6�   $��Bottom=�6�   $��66.775��6�   $��10(�6�   �� 5B   
   -   5        $ textBlock_Copy��5B      $��17.756��6,   $��10,129.244,0,0q�6<   $
��Wrap=�6T   $��Version: v0.0.1a��6h   $	��Top=�6�   
�������$��Yu Gothic UI Light)�6�   $��9(�6�   $
��Left=�6�   $��102.226��6�   $��Light"�6  �� 5C   
   -   5        $ button2_Copy��5C      $��注册��6'   #���  64   $��Right=�6g   $��24��6�   $��	0,0,17,10q�6�   $��Bottom=�6�   $��86.544��6�   ���	#FF404040$��12(�6�   5D      5E      