using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

    


namespace WTBot
{
    /// <summary>
    /// Interaction logic for ActivationWindow.xaml
    /// </summary>
    public partial class ActivationWindow : Window
    {

        bool isGenuine;
        const uint DaysBetweenChecks = 0;
        const uint GracePeriodLength = 0;
        public ActivationWindow()
        {
            //initializecomponent();
            //try
            //{
            //todo: goto the version page at limelm and paste this guid here
            //    ta = new turboactivate("vujjt35h32max2d33u5pds2v4l53n4y");

            //    check if we're activated, and every 90 days verify it with the activation servers
            //     in this example we won't show an error if the activation was done     offline
            //     (see the 3rd parameter of the isgenuine() function)
            //     https://wyday.com/limelm/help/offline-activation/

            //    isgenuineresult gr = ta.isgenuine(daysbetweenchecks, graceperiodlength, true);

            //    isgenuine = gr == isgenuineresult.genuine ||
            //                gr == isgenuineresult.genuinefeatureschanged ||

            //                 an internet error means the user is activated but
            //                 turboactivate failed to contact the limelm servers
            //                gr == isgenuineresult.interneterror;



                // If IsGenuineEx() is telling us we're not activated
                // but the IsActivated() function is telling us that the activation
                // data on the computer is valid (i.e. the crypto-signed-fingerprint matches the computer)
                // then that means that the customer has passed the grace period and they must re-verify
                // with the servers to continue to use your app.

                //Note: DO NOT allow the customer to just continue to use your app indefinitely with absolutely
                //      no reverification with the servers. If you want to do that then don't use IsGenuine() or
                //      IsGenuineEx() at all -- just use IsActivated().
    //            if (!isGenuine && ta.IsActivated())
    //            {
    //                // We're treating the customer as is if they aren't activated, so they can't use your app.
                    
    //                // However, we show them a dialog where they can reverify with the servers immediately.

    //                ReVerifyNow frmReverify = new ReVerifyNow(ta, DaysBetweenChecks, GracePeriodLength);

    //                if (frmReverify.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    //                {
    //                    isGenuine = true;
    //                }
    //                else if (!frmReverify.noLongerActivated) // the user clicked cancel and the user is still activated
    //                {
    //                    // Just bail out of your app
    //                    Environment.Exit(1);
    //                    return;
    //                }
    //            }

    //            if (!isGenuine)
    //            {
    //                //Console.WriteLine("Not activated"); 
    //            }
    //            else // genuine
    //            {
    //                MainWindow mw = new MainWindow();
    //                mw.Show();
    //                this.Hide();
    //            }
    //        }
    //        catch (TurboActivateException ex)
    //        {
    //            MessageBox.Show(Properties.Resources.errorOccured + ex.Message);
    //            Environment.Exit(1);
    //            return;
    //        }


        }

        //    private void invokeDisableVerifyButton()
        //    {
        //        System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
        //        {
        //            b_verify.IsEnabled = true;
        //        }));
        //    }


        private void b_verify_Click(object sender, RoutedEventArgs e)
        {
            //b_verify.IsEnabled = false;
            //string key = serial_key.Text;
            //Thread checkAuthentic = new Thread(() =>
            //{
            //    if (key.Equals(""))
            //    {
            //        invokeDisableVerifyButton();
            //        return;
            //    }
            //    try
            //    {
            //        bool success = ta.CheckAndSavePKey(key);
            //        if (success)
            //        {
            //            System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            //            {
            //                ta.Activate();
            //                MainWindow mw = new MainWindow();
            //                mw.Show();
            //                this.Hide();
            //            }));
            //        }
            //        else
            //        {
            //            MessageBox.Show(Properties.Resources.errorProductKeyNotValid);
            //            invokeDisableVerifyButton();
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.Message);
            //        invokeDisableVerifyButton();
            //    }
            //});
            //checkAuthentic.Start();
        }
        }
    }

