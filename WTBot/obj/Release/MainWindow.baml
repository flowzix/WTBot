   M S B A M L   `   `   `  ���� @  <WTBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WTBot.PropertiesWTBot.Properties  clr-namespace:WTBotWTBot      WTBot.MainWindow   $pclr-namespace:WTBot.Properties  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:WTBot     -     � Title$  WTBot-Friends&Family��$	��600��$	��800��$	��650��$	��500��.��� $��0,0,-0.4,0.4q�.+��� -    ��Name$ 
tabControl�����	#FFFAFAFA.@��� -    ��Name$ tabItemTasks������ -    ��Name$ tabItemTasksHeader��    WTBot.Properties.Resources   tabTasks#��Z .��� -   $��10,10,10,10q� � ColumnDefinitions �� $��3.75* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* � � RowDefinitions �� $
��1.5* ��� $��0.25* ��� $	��12* ��� $��1* ��� $��8* �.+��� -    ��Name$ lCapchasSolved��$��0��$��0��$��0��$��	0,0,5.2,5q�$��Right=�$��50��
���  ��$��2��$��Center=�$��Center=��� -    ��Name$ 
butAddTask��$��0��$��3��	   
butTaskAdd#��Z	 ���	#FF80DC80�� -   $ butEditTask��$��0��$��5��
   butTaskEdit#��Z
 ���	#FFDCDC80�� -	   $ butRemoveTask��$��0��$��7��   butTaskRemove#��Z ���	#FFDC8080�� -
    ��Name$ lvTasks��$��2��$��0��$��8��$��Hidden=� �� View �� -   gvTasks��.P���    taskName#��Z -   colTaskName�� �� Width$	 200����    taskInfoProfile#��Z -   
colProfile��$	 200����    taskInfoCheckoutDelay#��Z -   colCheckoutDelay��$	 200����    colRun#��Z -   
colRun��$	 150�� �� CellTemplate �� .R��� $ butStartTask��-   $��Start������  �� Path$ Width� �� ElementName$ colRun���� $��4��$��0��$��8��$��1q�
���   �._��� -    ��Name$ tbLogScroll��$��Visible=�.���� -   $ tbLog�� �� TextAlignment$
 Left=�$	��Top=�$
��Wrap=�$��1.2,1.2,8.2,0q�
������� J� RenderTransformOrigin$ 0.5,0.5&����� $ Width�$ lvTasks���� -   $ butSolveCaptchas��$��0��$��1��   butSolveCaptcha#��Z �� -   $ tabItemSettings������ -   $ tabItemSettingsHeader��   tabProfiles#��Z .��� -   $��10,10,10,10q� �� $��4* ��� $��6* ��� $��1* ��� $��3* ��� $
��1.5* ��� $��2* ��� $��2* � �� $	��52* ��� $	��52* ��� $	��52* ��� $	��52* ��� $	��52* ��� $	��52* ��� $	��53* ��� $	��52* ��� $	��53* ��� $��52 �.+��� -   $ labelPersonalData��$��0��$��0��$��2��   dataPersonal#��Z $
��Left=�$��Center=�$��20(�$
��Bold"��� -   $ labelFullName��$��1��$��0��   dataFullName#��Z $��Center=��� -   $ 
labelEmail��$��2��$��0��   	dataEmail#��Z $��Center=��� -   $ labelTel��$��3��$��0��   dataTel#��Z $��Center=��� -   $ labelAddress1��$��4��$��0��   dataAddress1#��Z $��Center=��� -   $ labelAddress2��$��5��$��0��    dataAddress2#��Z  $��Center=��� -   $ 	labelCity��$��6��$��0��!   dataCity#��Z! $��Center=��� -   $ labelPostcode��$��7��$��0��"   dataPostcode#��Z" $��Center=��� -   $ labelCountry��$��8��$��0��#   dataCountry#��Z# $��Center=��� -    $ ��Name$$ 
tbFullName��$��1��$��1��$��Center=�$��0,5,0,5q��� -!   $$ tbEmail��$��2��$��1��$��Center=�$��0,5,0,5q��� -"   $$ tbTel��$��3��$��1��$��Center=�$��0,5,0,5q��� -#   $$ 
tbAddress1��$��4��$��1��$��Center=�$��0,5,0,5q��� -$   $$ 
tbAddress2��$��5��$��1��$��Center=�$��0,5,0,5q��� -%   $$ tbCity��$��6��$��1��$��Center=�$��0,5,0,5q��� -&   $$ 
tbPostcode��$��7��$��1��$��Center=�$��0,5,0,5q��� -'   $ labelCardDetails��$��0��$��3��$��4��%   dataCard#��Z% $
��Left=�$��Center=�$��20(�$
��Bold"��� -(   $ labelCardType��$��1��$��3��&   dataCardType#��Z& $��Center=��� -)   $ labelCardNumber��$��2��$��3��'   dataCardNumber#��Z' $��Center=��� -*   $ labelCardExpire��$��3��$��3��(   dataCardExpire#��Z( $��Center=��� -+   $ labelCardCvv��$��4��$��3��)   dataCVV#��Z) $��Center=��� -,   $ labelCardRef��$��5��$��3��*   dataRef#��Z* $��Center=�$��0.2,4,0.2,0q��� --   + ��Name$+ 	cbCountry��$��8��$��1��$��Center=�$��0,5,0,5q�, �� SelectedValuePath$, Content��.@��� .��-�� .��UK�� .��UK (N. IRELAND)�� .��	AUSTRIA�� .��	BELARUS�� .��	BELGIUM�� .��
BULGARIA�� .��	CROATIA�� .��CZECH REPUBLIC�� .��	DENMARK�� .��	ESTONIA�� .��	FINLAND�� .��FRANCE�� .��	GERMANY�� .��GREECE�� .��	HUNGARY�� .��	ICELAND�� .��	IRELAND�� .��ITALY�� .��LATVIA�� .��	LITHUANIA�� .��
LUXEMBOURG�� .��MONACO�� .��NETHERLANDS�� .��NORWAY�� .��POLAND�� .��
PORTUGAL�� .��	ROMANIA�� .��RUSSIA�� .��
SLOVAKIA�� .��
SLOVENIA�� .��SPAIN�� .��SWEDEN�� .��SWITZERLAND�� .��TURKEY�� -.   $+ 
cbCardType��$��1��$��4��$��3��$��Center=�$��	4.8,5,0,5q�$	, Tag��.@��� 
- � Tag$- american_express��.��American Express�� $
- visa��.��Visa�� $- master��.��
Mastercard�� $
- solo��.��Solo�� $- paypal��.��PayPal�� -/   $$ tbCardno��$��2��$��4��$��3��$��Center=�$��	4.8,5,0,5q��� -0   $+ cbCardMonth��$��3��$��4��$��3��$��Center=�$��4.8,0,74,10q�$, Content��.@��� $- 01��.��01�� $- 02��.��02�� $- 03��.��03�� $- 04��.��04�� $- 05��.��05�� $- 06��.��06�� $- 07��.��07�� $- 08��.��08�� $- 09��.��09�� $- 10��.��10�� $- 11��.��11�� $- 12��.��12�� -1   $+ 
cbCardYear��$��3��$��6��$��1��$��Center=�$��
5.2,0,0,10q�$, Content��.@��� $
- 2018��.��2018�� $
- 2019��.��2019�� $
- 2020��.��2020�� $
- 2021��.��2021�� $
- 2022��.��2022�� $
- 2023��.��2023�� $
- 2024��.��2024�� $
- 2025��.��2025�� $
- 2026��.��2026�� $
- 2027��.��2027�� -2   $$ tbCVV��$��4��$��4��$��2��$��Center=�$��4.8,5,62.8,5q��� -3   $$ tbProfileName��$��5��$��4��$��3��$��Center=�$��
4.8,4,10,5q��� -4   $ butSave��$��6��$��3��$��2��.   dataSave#��Z. $��10.2,6,20.4,4.8q����	#FF80DC80�� -5   $ butClear��$��6��$��5��$��2��/   	dataClear#��Z/ $��9.6,6,10,4.8q��� -6   $ butEdit��$��7��$��3��$��2��#��Z
 $��10.2,5.2,20.4,4.8q����	#FFDCDC80�� -7   $ 	butRemove��$��7��$��5��#��Z $��9.6,5.2,10,4.8q�$��2�����	#FFDC8080�� -8   $ lvData��$��8��$��3��$��4��$��2��$��0.2,0.2,0,0q� �� -9   
gvData��.P��� #��Z* -:   dataColProfileName��$ 70���� -;   $ tabItemProxy������ -<   $ tabItemProxyHeader��0   tabProxyAndSettings#��Z0 .��� -=   1 �Name$1 proxyTabGrid��$��10,10,10,10q� �� $��1* ��� $
��0.1* ��� $��1* ��� $
��0.1* ��� $��1* ��� $
��0.1* ��� $��1* � �� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* ��� $��1* ��� $��0.25* �.+��� ->   $ lProxy��$��Proxy��$��0��$��0��$
��Left=�$��Center=�$��20(�$
��Bold"��� -?   $ 	lSettings��2   labelSettings#��Z2 $��0��$��12��$
��Left=�$��Center=�$��20(�$
��Bold"��� -@   $ 
lProxyName��3   
lProxyName#��Z3 $��0��$��2���� -A   $$ tbProxyName��$��2��$��2��$��Center=��� -B   $ lProxyAddress��4   lProxyAddress#��Z4 $��0��$��4���� -C   $$ tbProxyIpAddress��$��2��$��4��$��Center=��� -D   $ lProxyUsername��5   lProxyUsername#��Z5 $��0��$��6���� -E   $$ tbProxyUsername��$��2��$��6��$��Center=��� -F   $ lProxyPassword��6   lProxyPassword#��Z6 $��0��$��8���� -G   $$ tbProxyPassword��$��2��$��8��$��Center=��� -H   $ butProxyAdd��#��Z	 $��0��$��10�����	#FF80DC80�� -I   $ butProxyClear��#��Z/ $��2��$��10���� -J   $ butProxyEdit��#��Z
 $��4��$��10�����	#FFDCDC80�� -K   $ butProxyRemove��#��Z $��6��$��10�����	#FFDC8080�� -L   $ lvProxy��$��4��$��2��$��3��$��7��$��	0,0,0,0.4q� �� .P��� #��Z3 -M   colProxyName��$	 100���� $��IP��-N   	colIP��$	 100���� #��Z5 -O   colUsername��$	 100���� -P   $ lRefreshFreq��7   labelRefreshFreq#��Z7 $��0��$��14���� -Q   $$ 
tbRefresMS��$��2��$��14��$��Center=��� -R   $ butSettingsSave��#��Z. $��8��$��18��