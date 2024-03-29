﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using T1807EHello.Entity;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongList : Page
    {
        private ObservableCollection<Song> listSong { get; set; }
        private const string SongListUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-free-songs";
        public SongList()
        {
            this.InitializeComponent();
            this.listSong = new ObservableCollection<Song>();
            var songManager = new SongManagerImp();
            var responseContent = songManager.GetDataFromServer(SongListUrl);
            var songs = JsonConvert.DeserializeObject<List<Song>>(responseContent);
            foreach (var item in songs)
            {
                this.listSong.Add(new Song()
                {
                    name = item.name,
                    singer = item.singer,
                    thumbnail = item.thumbnail,
                    link = item.link,
                });
            }
        }
    }
}
