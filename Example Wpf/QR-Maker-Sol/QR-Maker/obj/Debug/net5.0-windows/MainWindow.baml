   M S B A M L   `   `   `  ����C  ?QR-Maker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QR_MakerQR_Maker  5   	       QR_Maker.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:QR_Maker     5   	   -   5          �Name$  window��5   7    � Title$ QR Generator��6	   $	��450��6   $	��800��6+   $	��707��6h   $	��434��6w   $
��1934��6�   $
��1080��6�    � 	Resources 5      &    QR_Maker.SizePercentageConverter   	SizeConvo   QR_Maker.FontSizeConvert 
 Fonter   QR_Maker.Stack  Hei   QR_Maker.TextBoxBinder 
 OneSix��%t   &	        &	      &	 &     &	 9       5   
   6(     5   
   6      5   
   6     5   
   6   5      .��5      � -   5         �Name$ 	colorGrid��5   "   
�����p�.+�5   
   �� -   5         ��Name$ qr��5   D   $	��300��6   $	��300��6   $��Bottom=�6)   5      �� 5   
   -   5         ��Name$ stack��5      .+�5      �� $	��221��6   $��50��6$   $��	UltraBold"�60   $	��Top=�6G   $��32(�6_   QR Generator6{   ]� 5      -   5         ]�Name$
 dock��5   I   $
��auto��6   $��45��6%   $	��Top=�61   .+�5      �� -   5         ��Name$ content��5   /   $��Right=�6�   $��18(�6�   $
��Left=�6�   $��Center=�6�   ��6@   ��  �� 	Converter# [ 	 �� ElementName$	 window��
 �� Path$
 ActualWidth�.��6  Hello world6"  �� 5      -   5         � Cursor$
 Hand��5      $��Right=�6'   
���   �$��3q�6W   
������ $	��100��6�   $��18(�6�   $��	UltraBold"�6�   .��6�   
Generate5!      5"      5#      �� 5%   
   $	��100��6   $��50��6!    �� Orientation$ 
Horizontal=�6-   $��Right=�6F   $��Bottom=�6b   .+�5&      �� -   5        $
 Hand��5&      
���   �$��3q�67   
������ $	��100��6d   $��18(�6p   $��	UltraBold"�6~   .��6�   Save5(      5)      �� 5+   
   -	   5        $ stacked��5+      $
��Left=�6�   $��Center=�6  ��6&   �� # [  $	 	colorGrid��$
 ActualWidth���6�   �� # [ $	 	colorGrid��$
 ActualHeight�.+�5-      �� $��QR White Color��6   $��DemiBold"�6  ��6.   �� # [  $	 	colorGrid��$
 ActualWidth���6�   �� # [ $	 stacked��$
 ActualWidth�6  �� 5/      -
   5         ��Name$ qrWhiteColor��5/   �   $��25��6�   $
��Left=�6�   $��Center=�6�   ��6   �� # [  $	 	colorGrid��$
 ActualWidth�51      �� 53      $��20��6   �� 55      $��QR Black Color��6   $
��Left=�6/   $��DemiBold"�6   ��6J   �� # [  $	 	colorGrid��$
 ActualWidth���6�   �� # [ $	 stacked��$
 ActualWidth�6   �� 57      -   5        $ qrBlackColor��57   �   $��25��6�   $
��Left=�6�   $��Center=�6�   ��6   �� # [  $	 	colorGrid��$
 ActualWidth�59      5;      �� 5B   
   $	��210��6   $
��auto��6    $
��Left=�6.   $��Bottom=�6I   $��16(�6d   $
��Bold"�6r   $
��Wrap=�6�   Made by BIGDummyHead�� 5E      on �� 5G      -   5        $%��http://github.com/BIGDummyHead/?�5G      .�6r   GitHub5I      5K      5M      5N      