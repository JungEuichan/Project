using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Diagnostics;
using System.Net;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;



namespace StockInfobot
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            //Trace.Indent(); //들여쓰기 시작

            //TextWriterTraceListener tr1 = new TextWriterTraceListener(System.Console.Out); //콘솔에 출력되는 부분
            //TextWriterTraceListener tr2 = new TextWriterTraceListener(System.IO.File.CreateText("Output.txt")); //파일로 출력되는 부분 -> StockInfoBot > bin > Debug에 파일이 출력되게 된다.

            //Trace.Listeners.Add(tr1);
            //Trace.Listeners.Add(tr2); //tr1과 tr2가 logging 내역을 받아오기 시작한다.

            //Trace.WriteLine("test for tracing");
            //Trace.Assert(3 < 1, "3은 1보다 작다는 틀린 명제입니다.");
            //Trace.Unindent(); //들여쓰기 종료

            //Trace.Flush(); //Output.txt파일에 debug를 기록한다.
            //InitializeComponent();

            //-----------------------셀레니움 관련 ------------------------------//

            driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            options = new ChromeOptions();
            options.AddArgument("disable-gpu");


            Login();
        }

        private Telegram.Bot.TelegramBotClient Bot = new Telegram.Bot.TelegramBotClient("1521193514:AAFgPlIWGkrFQnS-URMnk1mUJMUbuJFB0kU");



        protected ChromeDriverService driverService = null;
        protected ChromeOptions options = null;
        protected ChromeDriver driver = null;


        private void Form1_Load(object sender, EventArgs e)
        {
            

            //-----------------------텔레그램 관련 ------------------------------//

            telegramAPIAsync();
            Bot.OnMessage += Bot_OnMessage;       // 이벤트를 추가해줍니다.
            Bot.StartReceiving();        // 이 함수가 실행이 되어야 사용자로부터 메세지를 받을 수 있습니다.

            Bot.SendTextMessageAsync(1386485198, "PC에서 매매프로그램 구동이 시작되었습니다");
        }





        private void Login()
        {
             {
                //user-agent 수정
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--user-agent=Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5355d Safari/8536.25");
               // IWebDriver driver = new ChromeDriver(options);



                driver.Navigate().GoToUrl("http://10000img.com/"); // 웹 사이트에 접속합니다.
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                var cookies = driver.Manage().Cookies.AllCookies; // 모든 쿠키를 받아온다.


                //  var element = _driver.FindElementByTagName("img"); // "는 '로 바꿔서 써줄것


                driver.FindElement(By.XPath("//*[@id='u_0_0']/input[1]")).SendKeys("22190");
//                _driver.FindElement(By.XPath("//*[@id='u_0_0']/input[2]")).value = "AQFuhW_1TRwE:AQExqilOiAfb";

                // var element = _driver.FindElement(By.TagName("img"));
                // string url = element.GetAttribute("src");
                // Trace.WriteLine(url);

                //   WebClient myWebClient = new WebClient();
                //  myWebClient.DownloadFile("http://10000img.com/rimg3/hjf60.jpg", @"D:\image.jpg");
                // urlretrieve


                //string id = "sunbbb1234";
                //string pw = "n5807#flbb";
                //// _options.AddArgument("headless"); // 창을 숨기는 옵션입니다.

                //_driver = new ChromeDriver(_driverService, _options);
                //_driver.Navigate().GoToUrl("https://www.naver.com"); // 웹 사이트에 접속합니다.
                //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                //var element = _driver.FindElementByXPath("//*[@id='account']/a"); // "는 '로 바꿔서 써줄것
                //element.Click();

                //Task.Delay(TimeSpan.FromSeconds(3)).Wait(); ;

                //element = _driver.FindElementByXPath("//*[@id='id']");
                //element.SendKeys(id);

                //element = _driver.FindElementByXPath("//*[@id='pw']");
                //element.SendKeys(pw);

                //element = _driver.FindElementByXPath("//*[@id='log.login']");
                //element.Click();




                /*
                # 추가적인 셀레니움 이용법

                //텍스트박스 객체를 받아와서 text추출.
                var textBox = _driver.FindElementByXPath("//*[@id='nx_query']");
                string text = textBox.Text;

                아주 간단하죠? 그런데 분명히 텍스트 박스에 문자열이 존재하는데 위와 같이 실행하면 항상 공백("")이 얻어지는 경우가 있습니다. 이럴 때는 GetAttribute를 사용하여 값을 얻을 수 있습니다.
                string keyword = searchBox.GetAttribute("value");

                searchBox.Clear();


                //표의 셀에 접근
                개발자 도구로 테이블의 요소를 확인해 보면, table 키워드에 tbody가 존재하고, 그 안에 tr과 th가 존재합니다. tr은 행을, th는 헤드를 의미합니다.
                그리고 각 행의 열에 위치한 데이터는 td를 의미합니다.
                즉, table을 먼저 찾고 tbody를 찾은 다음 각 tr의 th 및 td를 접근하면 테이블의 모든 값을 확인할 수 있습니다.

                var table = _driver.FindElementByXPath("//*[@id='mw-content-text']/div[1]/table[2]");
                var tbody = table.FindElement(By.TagName("tbody")); //table에서 tbody를 찾습니다.
                var trs = tbody.FindElements(By.TagName("tr")); //tbody에서는 여러 개의 tr을 찾기 위해 FindElements를 사용합니다.

                // 찾은 결과물만큼 반복하면서 각 tr에 존재하는 th와 td의 데이터를 찾습니다. 아래 코드와 같습니다.
                foreach(var tr in trs)
                {
	                var ths = tr.FindElements(By.TagName("th"));
	                foreach(var th in ths)
	                {
		                Trace.WriteLine("th: " + th.Text);
	                }
 
	                var tds = tr.FindElements(By.TagName("td"));
	                foreach (var td in tds)
	                {
		                Trace.WriteLine("td: " + td.Text);
	                }
                }

                // 여러개의 객체를 받아와서 상호작용하는 법
                var buttons = datePicker.FindElements(By.TagName("a"));
                buttons[0].Click();
                buttons[1].Click();



                //iFrame 태그는 HTML 웹 페이지에 다른 웹 페이지가 추가된 것을 의미합니다. 
                iframe이란 inline frame의 약자입니다.
                iframe 요소를 이용하면 해당 웹 페이지 안에 어떠한 제한 없이 또 다른 하나의 웹 페이지를 삽입할 수 있습니다.

                문법
                <iframe src="삽입할페이지주소"></iframe>
                
                분명 개발자 도구로 요소를 확인했는데, 코드에서 접근할 때 찾을 수 없다는 에러가 발생된다면, 요소가 iFrame 태그에 존재하는지 확인해야 합니다.
                위 사이트의 iFrame 태그 XPath는 다음과 같습니다.
                //*[@id="content"]/iframe"
                iFrame 태그 및 메인 윈도우로 전환하는 코드는 다음과 같습니다.



                iFrame 태그 및 메인 윈도우로 전환하는 코드는 다음과 같습니다.

                string winHandleBefore = _driver.CurrentWindowHandle;
 
                //iFrame 찾기.
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                var iFrame = wait.Until(_drv => _drv.FindElement(By.XPath("//*[@id='content']/iframe")));
 
                //iFrame 전환.
                _driver.SwitchTo().Frame(iFrame);
 
                //iFrame에 존재하는 컨트롤 요소에 접근하기
                //...
                
                //메인 윈도우로 전환.
                _driver.SwitchTo().Window(winHandleBefore);







                //다운로드
                파일 다운로드입니다. headless 옵션을 설정하면 파일 다운로드가 동작하지 않습니다. 이 문제는 알려진 이슈이며 크롬 드라이버만 해당되는지는 모르겠습니다만, 해결하는 방법이 있습니다.
                아래와 같이 headless 옵션 외에 behavior 및 downloadPath를 지정해주면 되겠습니다.


                //ChromeDriverService 초기화
                ChromeDriverService _driverService = null;
                _driverService = ChromeDriverService.CreateDefaultService();
                _driverService.HideCommandPromptWindow = true;
 
                //ChromeOptions 초기화
                ChromeOptions _options = null;
                _options = new ChromeOptions();
                _options.AddArgument("headless"); 
 
                var param = new Dictionary<string, object>(); 
                param.Add("behavior", "allow"); 
 
                string downloadsPath = ""; //다운로드 경로 입력. 
                param.Add("downloadPath", downloadsPath); 
 
                //ChromeDriver 초기화 
                ChromeDriver _driver = null; 
                _driver = new ChromeDriver(_driverService, _options); 
 
                var result = ((OpenQA.Selenium.Chrome.ChromeDriver)_driver).ExecuteChromeCommandWithResult("Page.setDownloadBehavior", param); 
                _driver.Navigate().GoToUrl("http://www."); //웹 사이트 이동 
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout_sec);
                

                
                //   태그내부 텍스트를 바꿀때
                //     .Text        
                string sss = driver.FindElement(By.Id("breadcrumb")).Text;
                sss = sss.Replace("  >", "").Replace("/", "#").Replace("\r\n", "#");
                sss = sss.Replace(" ", "").Replace("#", " #");
                tag = sss.Replace("쿠팡홈", "");


                // 태그에 있는 class로 검색할 때
                 //   .FindElement(By.ClassName("어떤 태그의 class"));

                  
                //   해당 태그에 속해 있는 attribute(속성)을 검색
                //     .GetAttribute("속성이름")        
                string img = driver.FindElement(By.ClassName("prod-image__detail")).GetAttribute("src");
                mainimage = GetFileName(img);     




                 // 해당 클래스이름을 가진 태그가 있는지 체크하기        
                if (IsElementPresent(driver, By.ClassName("product-item__table")) == true)
                {
                    var element = driver.FindElement(By.ClassName("product-item__table"));
            
                    // 해당 클래스를 가진 html을 가져올 때            
                    // element.GetAttribute("innerHTML")            
                    var tempinfo = element.GetAttribute("innerHTML");
            
            
                    // 필요없는 부분은 split으로 쪼개서 버리기..            
                    string[] ss = tempinfo.Split(new string[] { "<p class=\"essential-info-more\">" }, StringSplitOptions.None);
                    productinfo = ss[0];
                }

                //이미지의 경우 해당 클래스 이름 가진 태그가 있는지?
                if(IsElementPresent(driver, By.ClassName("subType-IMAGE")) == true)
                { 
        	        // 똑같은 class 명을 가진 태그가 여러가지이며 전부 검색할 때
                    // .FindElemnts(By.ClassName("어떤 태그의 class))            
                    var productimage = driver.FindElements(By.ClassName("subType-IMAGE"));
                    int imageindex = 0;
                    image = new string[productimage.Count];
            
                    // 컬렉션이기 때문에 각각을 검색할 땐 foreach를 사용
                    foreach (var item in productimage)
                    {
                        var sub = item.FindElement(By.TagName("img")).GetAttribute("src");
                        image[imageindex] = GetFileName(sub);
                        imageindex++;
                    }
                }

                

                // 해당 정보를 가진 태그가 있는지 없는지 확인 
                private bool IsElementPresent(IWebDriver _driver, By _by)
                {
                    try
                    {
                        _driver.FindElement(_by);
                        return true;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                }


                element = driver.find_element_by_name('continue')
                element.click()
                element.click_and_hold()  # 누르고 있기
                element.release()         # 떼기
                element.double_click()    # 더블클릭 하기

                # 해당 element를 tartget으로 드래그 앤 드롭하기
                act.drag_and_drop(element,target).perform()

                https://www.guru99.com/keyboard-mouse-events-files-webdriver.html

                //스크린샷 찍기

                해당 페이지를 캡쳐해서 이미지로 저장하는 기능도 있다. 매우 간단하므로 예제만 보고 넘어가도록 하자.

                driver.get_screenshot_as_file('asd.png')


                //자바스크립트 실행
                # goPage(12) 메소드 실행
                driver.execute_script("goPage(12)")
                # title 반환하기
                driver.execute_script(‘return document.title;’)
                문자열에 자바스크립트 문법을 그대로 넣어주면 되니, 보다 능동적으로 크롤링이 가능하다.








                Element 를 찾는 방법에는 여러가지가 있는데 보통 세가지 정도면 모두 커버할 수 있다.
                1. ID 로 찾기
                웹 브라우저에 출력되는 element 들은 ID 라는 것을 갖는다. ID 는 크롬 등의 브라우져에서 F12 버튼을 눌러서 찾을 수 있다.
                아래 그림은 Tistory 로그인 화면에서 에서 F12 눌렀을 때 출력되는 항목을 보여준다.
                그 상태에서 Control + Shift + C 를 누른 다음에 ID 입력 창을 클릭하면 아래와 같이 화면이 출력된다.
                그림에서 보는 바와 같이 ID 창에 loginId 입력을 할 수 있음이 나오고 F12 를 눌러서 나온 개발자 화면에 해당 항목의 id 가 loginId 임이 표시된다.
                위와 같이 element 의 ID 를 찾을 수가 있다.
                그런 다음 코드에서 아래와 같이 입력하면 현재 출력된 웹 페이지에서 ID 가 'loginId' 인 것을 찾을 수 있다.
                var element = driver.FindElement(By.Id("loginId"));
 

                2. ClassName 으로 찾기
                눈치가 빠른 사람은 위의 설명에서 이미 봤을지도 모르겠다.
                ID 입력 창을 선택한 화면을 다시 보면 의 부모에
                라고 써 있는 것을 볼 수 있다.
                그렇다!!! 그것이 바로 ClassName 이다.
                코드에서 아래와 같이 입력하면 특정 ClassName 을 갖는 element 를 찾을 수 있다.
                element = driver.FindElement(By.ClassName("btn_login"));
 

                3. XPath 로 찾기
                솔직히 별로 효과적인 방법은 아니다...언제...path 가 변경될 지 알 수 없기 때문이다...






                뭔가 로딩이 안된다 싶으면 새로고침하기
                Refresh();



                */
            }
        }


        // 라인(한 줄) == 찾는 텍스트 일 때
        private bool FindLineText(string text)
        {
            bool b = false;
            try
            {
                string filename = "D:\\test.txt";

                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;

                    // 텍스트파일 한줄씩 읽기
                    while ((line = sr.ReadLine()) != null)
                    {
                        // 해당 문자가 있으면 브레이크
                        if (line == text)
                        {
                            b = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return b;
        }




        // 텍스트파일 전체에서 문자 찾기
        private bool FindText(string text)
        {
            bool b = false;
            try
            {
                string filename = "D:\\test.txt";

                using (StreamReader sr = new StreamReader(filename))
                {
                    // 텍스트파일 전체 읽기
                    string s = sr.ReadToEnd();

                    // 문자가 포함되는지 확인
                    if (s.Contains(text) == true)
                    {
                        b = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return b;
        }



        // init methods...
        private async void telegramAPIAsync()
        {
            //Token Key 를 이용하여 봇을 가져온다.
            var Bot = new Telegram.Bot.TelegramBotClient("1521193514:AAFgPlIWGkrFQnS-URMnk1mUJMUbuJFB0kU");
            //Bot 에 대한 정보를 가져온다.
            var me = await Bot.GetMeAsync();
            //Bot 의 이름을 출력한다.
           //  MessageBox.Show("Hello my name is " + me.FirstName);
        }


        private async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            
            var message = e.Message;

            string explanation = "안녕하세요 StockInfo_bot입니다 :) \n해당 봇은 사용자 대신 매수매도를 대행하며, 매수매도 전에 사용자에게 의사를 물어보는 용도로 제작되었습니다. \n그 외에 프로그램 구동 및 주가에 관한 정보를 받는 용도로도 사용됩니다 \n봇을 이용하기 위한 사용자의 chat_Id는 다음과 같습니다" + message.Chat.Id;

            if (message == null || message.Type != MessageType.Text) return;

            if(message.Text == "@explain")
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, explanation);
            }
        }






        //public class TelegramBot
        //{
        //    private static readonly string _baseUrl = "https://api.telegram.org/bot";
        //    private static readonly string _token = "1521193514:AAFgPlIWGkrFQnS-URMnk1mUJMUbuJFB0kU";
        //    public static string _chatId = string.Empty;

        //    /// <summary>
        //    /// 텔레그램봇에게 메시지를 보냅니다.
        //    /// </summary>
        //    /// <param name="text">보낼 메시지</param>
        //    /// <param name="errorMessage">오류 메시지</param>
        //    /// <returns>결과</returns>
        //    public static bool SendMessage(string text, out string errorMessage)
        //    {
        //        return SendMessage(_chatId, text, out errorMessage);
        //    }

        //    /// <summary>
        //    /// 텔레그램봇에게 메시지를 보냅니다.
        //    /// </summary>
        //    /// <param name="chatId">chat id</param>
        //    /// <param name="text">보낼 메시지</param>
        //    /// <param name="errorMessage">오류 메시지</param>
        //    /// <returns>결과</returns>
        //    public static bool SendMessage(string chatId, string text, out string errorMessage)
        //    {
        //        string url = string.Format("{0}{1}/sendMessage", _baseUrl, _token);

        //        HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
        //        req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
        //        req.Timeout = 30 * 1000;
        //        req.Method = "POST";
        //        req.ContentType = "application/json";

        //        string json = String.Format("{{\"chat_id\":\"{0}\", \"text\":\"{1}\"}}", chatId, EncodeJsonChars(text));
        //        byte[] data = UTF8Encoding.UTF8.GetBytes(json);
        //        req.ContentLength = data.Length;
        //        using (Stream stream = req.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //            stream.Flush();
        //        }

        //        HttpWebResponse httpResponse = null;
        //        try
        //        {
        //            httpResponse = req.GetResponse() as HttpWebResponse;
        //            if (httpResponse.StatusCode == HttpStatusCode.OK)
        //            {
        //                string responseData = null;
        //                using (Stream responseStream = httpResponse.GetResponseStream())
        //                {
        //                    using (StreamReader reader = new StreamReader(responseStream, UTF8Encoding.UTF8))
        //                    {
        //                        responseData = reader.ReadToEnd();
        //                    }
        //                }

        //                if (0 < responseData.IndexOf("\"ok\":true"))
        //                {
        //                    errorMessage = String.Empty;
        //                    return true;
        //                }
        //                else
        //                {
        //                    errorMessage = String.Format("결과 json 파싱 오류 ({0})", responseData);
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                errorMessage = String.Format("Http status: {0}", httpResponse.StatusCode);
        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            errorMessage = ex.Message;
        //            return false;
        //        }
        //        finally
        //        {
        //            if (httpResponse != null)
        //                httpResponse.Close();
        //        }
        //    }

        //    private static string EncodeJsonChars(string text)
        //    {
        //        return text.Replace("\b", "\\\b")
        //            .Replace("\f", "\\\f")
        //            .Replace("\n", "\\\n")
        //            .Replace("\r", "\\\r")
        //            .Replace("\t", "\\\t")
        //            .Replace("\"", "\\\"")
        //            .Replace("\\", "\\\\");
        //    }
        //}


        //private void Telegram_Send()
        //{
        //    string text = Messge;
        //    string errorMessage = null;
        //    bool ret = TelegramBot.SendMessage(text, out errorMessage);


        //    switch (TelegramBot._chatId)
        //    {
        //        case "chatId1":
        //            Who = "사용자1";
        //            break;
        //        case "chatId2":
        //            Who = "사용자2";
        //            break;
        //        case "chatId3":
        //            Who = "사용자3";
        //            break;
        //        case "chatId4":
        //            Who = "사용자4";
        //            break;
        //        case "chatId5":
        //            Who = "사용자5";
        //            break;
        //    }

        //    //Log 기록.
        //    TelegramBot._chatId = "chatId_Bot";
        //    text = "[" + Who + "] 전송시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    bool ret2 = TelegramBot.SendMessage(text, out errorMessage);

        //}



    }
}
