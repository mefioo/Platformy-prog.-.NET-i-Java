   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab01Lab01  5   	       Lab01.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab01     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      .+�5   
   �� $��20q�6   ��6   �� *Items+��5      �� 5      .R�5      ��  ��Name$ Panel��6!   -   5         �� Orientation$ 
Horizontal=�5   0   {�. .+�5      ��  ��Name$ 
Textblock1��6$   $	��100��6O   ��68   �� *Text1+6O   �� 5      $ 
Textblock2��6$   $	��100��6O   ��68   �� *Text2+6O   �� 5      $��50��69   $��50��6D   ��6    �� *Text3+6D   5      5      5      �� 5   
   -   5         ��Name$ addNewPersonButton��5      $	��Add��6-   $��1��6;   $
��Left=�6K   $��301,379,0,0q�6f   $	��Top=�6{   $��75��6�   6�   �� 5   
   -   5         ��Name$ TextBox2��5      $��1��6$   $
��Left=�64   $��23��6O   $��
31,351,0,0q�6[    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $	��345��6�   �� 5   
   $��1��6   $
��Left=�6$   $��
31,330,0,0q�6?   $
��Wrap=�6S   $��Second text��6g   $	��Top=�6z    J� RenderTransformOrigin$ 0.303,0.573&�6�   $��83��6�   �� 5   
   -   5        $ TextBox1��5      $��1��6$   $
��Left=�64   $��23��6O   $��
31,302,0,0q�6[   $
 Wrap=�6o   $	��Top=�6�   $	��345��6�   �� 5   
   $��1��6   $
��Left=�6$   $��
31,281,0,0q�6?   $
��Wrap=�6S   $��
First text��6g   $	��Top=�6y   $��75��6�   �� 5   
   
���   �$��1q�6%   $��1��69   $
��Left=�6I   $	��246��6d   $��	31,20,0,0q�6q   $	��Top=�6�   $	��253��6�   ._�5       �� -   5        	 ��Name$	 imgPhoto��5    $   $��1��6   6$   5!      �� 5"   
   -   5        $ Loadbtn��5"      $
��Load��6"   $��1��61   $
��Left=�6A   $��301,246,0,0q�6\   $	��Top=�6q   $��75��6�   6�   5&      5'      