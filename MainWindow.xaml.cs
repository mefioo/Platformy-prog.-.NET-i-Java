using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Net;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using LiveCharts.Wpf;
using LiveCharts.Geared;

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();

        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
            // new Person { Text1 = "P1", Text2 = "P11", Text3 = "im5.jpg" },
            // new Person { Text1 = "P2", Text2 = "P22", Text3 = "im2.jpg" },
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        Entity_Data_Modells.WeatherEntities db = new Entity_Data_Modells.WeatherEntities();
        System.Windows.Data.CollectionViewSource weatherEntryViewSource;
        System.Windows.Data.CollectionViewSource weatherEntitiesViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            weatherEntryViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("weatherEntryViewSource")));
            weatherEntitiesViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("weatherEntitiesViewSource")));

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.WeatherEntries.Local.Concat(db.WeatherEntries.ToList());
            weatherEntryViewSource.Source = db.WeatherEntries.Local;
            weatherEntitiesViewSource.Source = db.WeatherEntries.Local;
            System.Windows.Data.CollectionViewSource personViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // personViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource productViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // productViewSource.Source = [generic data source]
        }
        // Ogólnie tutaj sobie odpalam google searcha
        private string GetHtmlCode(string search_description)
        {
            var rnd = new Random();

            string url = "https://www.google.com/search?q=" + search_description + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;

        }

        //Getting list of images by tags
        async private Task<List<string>> GetUrls(string html)
        {
            var urls = new List<string>();
            int ndx = html.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
            ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                ndx = ndx + 5;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);
            }
            return urls;
        }

        //Adding single image to bytearray to download it ( can actually send it to the table with data, but we are using path instead of Image.Source)
        async private Task<byte[]> GetImage(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    byte[] bytes = sr.ReadBytes(100000);

                    return bytes;
                }
            }
        }
        async public Task<string> GetRandomImage(string search_description)
        {
            string html = GetHtmlCode(search_description);
            List<string> urls = await GetUrls(html);
            var rnd = new Random();

            int randomUrl = rnd.Next(0, urls.Count - 1);

            string luckyUrl = urls[randomUrl];

            byte[] image = await GetImage(luckyUrl);

            using (var stream = new MemoryStream(image))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                int random_number = rnd.Next(50000);
                string returned_path = random_number.ToString() + "imnet" + randomUrl.ToString() + ".bmp";
                try
                {
                    using (var fileStream = new System.IO.FileStream(returned_path, System.IO.FileMode.Create))
                    {
                        encoder.Save(fileStream);
                        return returned_path;
                    }
                }
                catch
                {
                    return returned_path;
                }

            }
        }

        async Task<int> GetNumberAsync(int number)
        {
            string search_description = goglego.Text.Replace(" ", "+");
            if (search_description.Length == 0) search_description = "stop+sign";
            if (number < 0)
                throw new ArgumentOutOfRangeException("number", number, "The number must be greater or equal zero");
            int result = 0;
            string my_path = "default_image.jpg";
            var randomizer = new Random();
            int random_id = randomizer.Next(10000);
            while (result < number)
            {
                try
                {
                    my_path = await GetRandomImage(search_description);
                }
                catch (Exception)
                {
                    throw new TimeoutException("Image couldn't be found, check your internet connection");
                }
                people.Add(new Person { Text1 = my_path, Text2 = random_id, Text3 = Directory.GetCurrentDirectory() + "/" + my_path });
                result += 1;
                await Task.Delay(2000);
            }

            return number;
        }

        string path;
        private void Loadbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
            path = op.FileName;

        }
        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddPersonPeople(new Person { Text2 = int.Parse(TextBox2.Text), Text1 = TextBox1.Text, Text3 = path });
            }
            catch (Exception)
            {
                throw new InvalidDataException("Something were wrong with data. Check if there is a number in second box");
            }
        }

        private void AddPerson(Person person)
        {
            Application.Current.Dispatcher.Invoke(() => { Items.Add(person); });
        }

        private void AddPersonPeople(Person person)
        {
            Application.Current.Dispatcher.Invoke(() => { people.Add(person); });
        }


        private void Panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var test = sender as StackPanel;

            var txt1 = ((System.Windows.Controls.TextBlock)test.Children[0]).Text;
            TextBox1.Text = txt1;
            var txt2 = ((System.Windows.Controls.TextBlock)test.Children[1]).Text;
            TextBox2.Text = txt2;
            var img = ((System.Windows.Controls.Image)test.Children[2]).Source;
            imgPhoto.Source = img;
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            int finalnumber = 0;
            try
            {
               finalnumber = int.Parse(Textbox3.Text);
            }
            catch (Exception)
            {
                throw new InvalidDataException("Wrong input number");
            }
            var getResultTask = GetNumberAsync(finalnumber);
            int result = await getResultTask;

        }

        private async void Load_data_Click(object sender, RoutedEventArgs e)
        {
            string responseXML = await WeatherConnection.LoadDataAsync("London");
            WeatherData result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseXML)))
            {
                result = ParseWeather_XmlReader.Parse(stream);
                Items.Add(new Person()
                {
                    Text1 = "Parser: " + result.City,
                    Text2 = (int)Math.Round(result.Temperature)
                });
            }

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseXML)))
            {
                result = ParseWeather_LINQ.Parse(stream);
                Items.Add(new Person()
                {
                    Text1 = "Linq: " + result.City,
                    Text2 = (int)Math.Round(result.Temperature)
                });
            }

            if (worker.IsBusy != true)
                worker.RunWorkerAsync();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            weatherDataProgressBar.Value = e.ProgressPercentage;
            weatherDataTextBlock.Text = e.UserState as string;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            List<string> cities = new List<string> {
                "London", "Warsaw", "Paris", "London", "Warsaw" };
            for (int i = 1; i <= cities.Count; i++)
            {
                string city = cities[i - 1];

                if (worker.CancellationPending == true)
                {
                    worker.ReportProgress(0, "Cancelled");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    worker.ReportProgress(
                        (int)Math.Round((float)i * 100.0 / (float)cities.Count),
                        "Loading " + city + "...");
                    string responseXML = WeatherConnection.LoadDataAsync(city).Result;
                    WeatherData result;

                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseXML)))
                    {
                        result = ParseWeather_XmlReader.Parse(stream);
                        AddPerson(new Person()
                        {
                            Text1 = "Linq: " + result.City,
                            Text2 = (int)Math.Round(result.Temperature)
                        });
                    }
                    Thread.Sleep(2000);
                }
            }
            worker.ReportProgress(100, "Done");
        }

        private void WeatherEntryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newEntry = new Entity_Data_Modells.WeatherEntry()
            {
                Id = int.Parse(Idtext.Text),
                City = Place.Text,
                Temperature = double.Parse(Temp.Text)
            };
            db.WeatherEntries.Local.Add(newEntry);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.WeatherEntries.Local.Remove(newEntry);
            }
        }

        async private void City_download(object sender, RoutedEventArgs e)
        {
            string city_to_search = City_search.Text;
            string image_path;
            string responseXML = await WeatherConnection.LoadDataAsync(city_to_search);
            WeatherData result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseXML)))
            {
                result = ParseWeather_XmlReader.Parse(stream);

                try
                {
                    image_path = await GetRandomImage(result.description + "+" + result.City);
                }
                catch (Exception)
                {
                    throw new TimeoutException("Image couldn't be found, check your internet connection");
                }

                Items.Add(new Person()
                {
                    Text1 = result.City,
                    Text2 = (int)Math.Round(result.Temperature),
                    Text3 = Directory.GetCurrentDirectory() + "/" + image_path
                });
            }

        }

        private void Plotit(object sender, RoutedEventArgs e)
        {
            plottitle.Text = "Temperature in " + cityplot.Text;
            var r = new Random();
            var data = db.WeatherEntries.Local.OrderBy(x => x.Id).ToList();
            int lenght = data.Where(x => x.City == cityplot.Text).Count();
            var values = new double[lenght];
            var labels = new double[lenght];
            for (int j =0; j< lenght; j++)
            {
                values[j] = data[j].Temperature;
                labels[j] = data[j].Id;
            }
            mychartt.Series = new SeriesCollection();

            var series = new LineSeries
            {
                Title = cityplot.Text,
                Values = values.AsGearedValues().WithQuality(Quality.Low),
                Fill = Brushes.Transparent,
                StrokeThickness = .5,
                PointGeometry = null //use a null geometry when you have many series
            };
            mychartt.Series.Add(series);
            
        }
    }
}
