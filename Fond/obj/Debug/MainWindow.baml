   M S B A M L   `   `   `  ����?  ;Fond, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:FondFond  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5   	       Fond.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Fond     5   	   Bcol2clr-namespace:System.Collections;assembly=mscorlib     5   	     � Title$  
MainWindow��5	   	   $	��500��6   $
��1100��6)    � WindowState$ 	Maximized=�66    � DataContext 5         Fond.FondsData  5   
    � 	Resources 5         DataGridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property  ColumnHeaderStyle �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   &	        &	 X     �� 5   
   "  6&   .�5      ��  �@ ColumnHeaderStyle# �  62   5      �� 5   
   "  6*   .�5      ��  � ��$
 Bold"�6+   �� 5       � ��$ 16(�6)   5      5      .��5      � .+�5   
     -   5          Name$ GridFond��5         AutoGenerateColumns .  	   RowBackground
	 �����
   	RowHeight$
 30Q�6X      HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����#���   5   ;      
IsReadOnly . ��6   �� *FondParamCollection+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6I      
Visibility$ Hidden=�6T      
FontFamily$ 	Arial Cyr)�6h      FontSize$ 15Q�6   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+6     5      $ Год��6%   $ 60 6J   $ 	Arial Cyr)�6U   $ 15Q�6m    62   �� *God+6m     5       $! Номер договора��6%   $	 140 6W   $ 	Arial Cyr)�6c   $ 15Q�6{    6=   �� *Nomer+6{     5!      $ Название��6%   $
 1200 6P   $ 	Arial Cyr)�6]   $ 15Q�6u    67   �� *Name+6u     5"      $ Организация��6%   $
 auto 6[   $ 	Arial Cyr)�6h   $ 15Q�6    6:   �� *Organization+6   5%      5&      5)      