using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Drawing.Drawing2D;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

using System.Net.Http;

using FastReport.Utils;
using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Pdf;
using FastReport.Cloud;
using FastReport.Cloud.Management;
using FastReport.Cloud.ReportProcessor;
using FastReport.Cloud.ResultsProvider;

using CSharpSDKDemo;

namespace DangerousSausages
{
    
public partial class MainWindow : Window
    {
        string Token;
        private static Telegram.Bot.TelegramBotClient BOT;
        public  MainWindow()
        {
            InitializeComponent();
        }

        private void EnterToken(object sender, EventArgs e)
        {
            Token = TokenText.Text;

            if (TokenText != null)
            {
                TokenLabel.Content = "Token received";
            }
        }

        private void StartBot(object sender, RoutedEventArgs e)
        {
            if (TokenLabel.Content.Equals("Token received"))
            {
                TokenLabel.Content = "Bot alive";
            }
            BOT = new Telegram.Bot.TelegramBotClient(Token);
            BOT.OnMessage += BotOnMessageReceived;
            BOT.StartReceiving(new UpdateType[] { UpdateType.Message });
        }



        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            String Answer = "";
            string[] arrayChar = {
                "https://www.meme-arsenal.com/memes/382906c071b1656c03104e98775b99b7.jpg",
                "https://www.meme-arsenal.com/memes/c27ee50cd596b0bd73ce01b543246de6.jpg",
                "https://pbs.twimg.com/media/EVqEsClXsAQ7Mio.jpg",
                "https://cs.pikabu.ru/post_img/2013/09/01/8/1378039082_293717672.jpg",
                "https://i.pinimg.com/originals/5f/67/a6/5f67a64f65f27821a81a9193c97f9573.jpg"

            };

            Telegram.Bot.Types.Message msg = messageEventArgs.Message;
            //if (msg == null || msg.Type != MessageType.Document) return;
            //{
            //    Console.WriteLine(msg.Document.FileId);
            //    var file = await BOT.GetFileAsync(msg.Document.FileId);
            //    FileStream fs = new FileStream("C:\\Users\\anton\\OneDrive\\Рабочий стол\\DangerousSausages\\file.pdf", FileMode.Create);
            //    await BOT.DownloadFileAsync(file.FilePath, fs);
            //    fs.Close();
            //    fs.Dispose();
            //}

            if (msg == null || msg.Type != MessageType.Text) return;
            switch (msg.Text)
                {

                    case "/start":
                        Answer = "Чтобы узнать, что я умею напишите /help\n";
                        break;
                    case "/help":
                    Answer = "Бот не может работать по следующему сценарию:\n\n" +
                    "1.отправляется файл frx на экспорт\n" +
                    "2.экспортируется в некий формат, например pdf\n" +
                    "3.возвращается\n\n" +
                    "Но зато, есть секретик на команду /memes\n";
                    break;
                    case "/status":
                        Answer = "Пока не работает остальное, мы не можем добавить данную функцию\n";
                        break;
                    case "/memes":
                    Answer = "Поздравляю, вы нашли ржумбу и открыли панель с клавишей)\n";
                    break;
                case "Скрыть панель":
                    Answer = "напишите команду заново";
                    await BOT.SendTextMessageAsync(msg.Chat.Id, "Для повторного вызова панели", replyMarkup: new ReplyKeyboardRemove());
                    break;
                case "Мемесы:)":
                            Answer = "Ржумба";
                            await BOT.SendPhotoAsync(msg.Chat.Id, arrayChar[new Random().Next(0, arrayChar.Length)], " ");
                            break;
                default:
                    Answer = "В разработке";
                        break;
                }
            await BOT.SendTextMessageAsync(msg.Chat.Id, Answer);
            

            if (msg.Text == "/memes")
            {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] {

                                                new[] // row 5
                                                {
                                                    new KeyboardButton("Мемесы:)"),
                                                },
                                                new[] // row 6
                                                {
                                                    new KeyboardButton("Скрыть панель"),
                                                },
                                             },
                    ResizeKeyboard = true
                };
                await BOT.SendTextMessageAsync(msg.Chat.Id, "Нажмите на клавишу, чтобы получить мемчик", replyMarkup: keyboard);
            }
        }
        private void TokenText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      
    }
    }
