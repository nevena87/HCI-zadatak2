   M S B A M L   `   `   `  ����I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NetworkServiceNetworkService  5   	   ;"clr-namespace:NetworkService.ViewsNetworkService.Views  5      C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5          NetworkService.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:NetworkService     5   	   4views"clr-namespace:NetworkService.Views     5      <	viewModel&clr-namespace:NetworkService.ViewModel     5      -   5        $��MainWindowRef��5	        � Title$  Network Service��5
   	   $	��450��6!   $	��500��6.    � WindowStyle$
 None=�6;    � 
ResizeMode$ NoResize=�6N    � DataContext 5      2   ,NetworkService.ViewModel.MainWindowViewModel  5   
    � 	Resources 5       �� DataType,   &NetworkService.ViewModel.HomeViewModel#   NetworkService.Views.HomeView7   1NetworkService.ViewModel.NetworkEntitiesViewModel.   (NetworkService.Views.NetworkEntitiesView6   0NetworkService.ViewModel.NetworkDisplayViewModel-   'NetworkService.Views.NetworkDisplayView8   2NetworkService.ViewModel.MeasurementGraphViewModel/	   )NetworkService.Views.MeasurementGraphView   BackgroundBrush �� ImageSource��%{  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)&	  �     �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5      	  5      �� 5   
   $1 +/NetworkService;component/Assets/putevi.png��6-   5      .��5      � #��[  6    � RowDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   .+�5#   
   ]� $��0��6   $��0,0,0,0q�6!   $��35��62   .+�5$      o� $	��Top=�6   .@�5%      m�  m� CommandParameter$
 Home��6:   $��
15,2.5,0,0q�6R   	 m� Command	 6   �� *
NavCommand+��5&      �� 5'      $��/Assets/home.png��6    $��30��6;   $��30��6F   5)      m� 5*      $ NetworkEntities��6:   $��
60,2.5,0,0q�6]   	 6   �� *
NavCommand+��5+      �� 5,      $��/Assets/listAdd.png��6    $��30��6=   $��30��6H   5.      m� 5/      $ NetworkDisplay��6:   $��
60,2.5,0,0q�6\   	 6   �� *
NavCommand+��50      �� 51      $��/Assets/dragDrop.png��6    $��30��6>   $��30��6I   53      m� 54      $ MeasurementGraph��6:   $��
60,2.5,0,0q�6^   	 6   �� *
NavCommand+��55      �� 56      $"��/Assets/measurementGraph.png��6    $��30��6F   $��30��6Q   58      m� 59      $��
60,2.5,0,0q�6s   	 6   �� *CloseCommand+ 6<   �� 
 �� ElementName$
 MainWindowRef����5:      �� 5;      $��/Assets/close.png��6    $��30��6;   $��30��6F   5=      5>      5?      � 5A   
   $��1��6   .+�5B      �� ��6   �� *CurrentViewModel+5C      5D      5E      