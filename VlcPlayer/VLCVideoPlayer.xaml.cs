using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Declarations;
using Declarations.Events;
using Declarations.Media;
using Declarations.Players;
using Implementation;
using Renderer.Core;
using System.Runtime.InteropServices;

namespace RigourTech.Football.UserControls
{
    /// <summary>
    /// VLCVideoPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class VLCVideoPlayer : UserControl
    {
        [DllImport("msvcrt.dll")]
        static extern int memcpy(IntPtr ptr1, IntPtr ptr2, int count);

        IMediaPlayerFactory m_factory;
        IVideoPlayer m_player;
        IMediaFromFile m_media;
        IMedia m_mediaRTMP;
        private BitmapFormat m_format;
        private IMemoryRendererEx m_source;
        D3DImageSource d3dSource;

        private EventHandler<MediaDurationChange> _DurationChanged;

        byte[] m_ptr_buffer;

        public VLCVideoPlayer()
        {
            InitializeComponent();

            m_factory = new MediaPlayerFactory(true);
            m_player = m_factory.CreatePlayer<IVideoPlayer>();
            Initialize(m_player.CustomRendererEx);
        }

        public EventHandler PlayerStopped
        {
            set
            {
                m_player.Events.PlayerStopped += value;
            }
        }

        public EventHandler<MediaPlayerPositionChanged> PlayerPositionChanged
        {
            set
            {
                m_player.Events.PlayerPositionChanged += value;
            }
        }

        public EventHandler<MediaDurationChange> DurationChanged
        {
            set
            {
                _DurationChanged = value;
            }
        }

        public EventHandler<MediaPlayerTimeChanged> TimeChanged
        {
            set
            {
                m_player.Events.TimeChanged += value;
            }
        }

        public bool IsPlaying
        {
            get
            {
                return m_player.IsPlaying;
            }
        }

        public bool Paused
        {
            get
            {
                if (m_media == null)
                    return false;
                return m_media.State == Declarations.MediaState.Paused;
            }
        }

        public float Duration
        {
            get
            {
                return m_media.Duration;
            }
        }

        public float Position
        {
            get
            {
                return m_player.Position;
            }
            set
            {
                m_player.Position = value;
            }
        }

        public long Time
        {
            get
            {
                return m_player.Time;
            }
            set
            {
                m_player.Position = value;
            }
        }

        public void PlayFile(string path)
        {
            this.imageD3D.Source = this.d3dSource.ImageSource;

            m_media = m_factory.CreateMedia<IMediaFromFile>(path);

            if(_DurationChanged != null)
                m_media.Events.DurationChanged += _DurationChanged;

            m_player.Open(m_media);
            m_media.Parse(false);

            m_player.Play();
        }

        public void PlayFile(string path, bool isplay)
        {
            this.imageD3D.Source = this.d3dSource.ImageSource;

            m_media = m_factory.CreateMedia<IMediaFromFile>(path);

            if (_DurationChanged != null)
                m_media.Events.DurationChanged += _DurationChanged;

            m_player.Open(m_media);
            m_media.Parse(false);

            if(isplay)
                m_player.Play();
        }

        public void PlayStreaming(string url)
        {
            this.imageD3D.Source = this.d3dSource.ImageSource;

            m_mediaRTMP = m_factory.CreateMedia<IMedia>(url);

            m_player.Open(m_mediaRTMP);
            m_player.Play();
        }

        public void PlayStreaming(string url,bool isplay)
        {
            this.imageD3D.Source = this.d3dSource.ImageSource;

            m_mediaRTMP = m_factory.CreateMedia<IMedia>(url);

            m_player.Open(m_mediaRTMP);

            if(isplay)
                m_player.Play();
        }

        public void Play()
        {
            m_player.Play();
        }

        public void Pause()
        {
            m_player.Pause();
        }

        public void Stop()
        {
            Task.Factory.StartNew(() =>
            {
                if (m_player.IsPlaying)
                    m_player.Stop();
            });
        }

        public EventHandler MediaEnded
        {
            set
            {
                m_player.Events.MediaEnded += value;
            }
        }

        public void Clear()
        {
            imageD3D.Source = null;
        }

        private void Initialize(IMemoryRendererEx renderer)
        {
            m_source = renderer;
            m_source.SetFormatSetupCallback(OnFormatSetup);
            m_source.SetCallback(OnNewFrame);
            this.d3dSource = new D3DImageSource();
        }

        private BitmapFormat OnFormatSetup(BitmapFormat format)
        {
            m_format = format;

            if (this.d3dSource.SetupSurface(format.Width, format.Height, FrameFormat.YV12))
            {

                int imgsize = (int)(m_format.Width * m_format.Height * 1.5F);
                m_ptr_buffer = new byte[imgsize];
            }

            return new BitmapFormat(format.Width, format.Height, ChromaType.I420);
        }

        private void OnNewFrame(PlanarFrame frame)
        {
            Marshal.Copy(frame.Planes[0], m_ptr_buffer, 0, m_format.Width * m_format.Height);
            Marshal.Copy(frame.Planes[2], m_ptr_buffer, m_format.Width * m_format.Height, m_format.Width * m_format.Height / 4);
            Marshal.Copy(frame.Planes[1], m_ptr_buffer, m_format.Width * m_format.Height + m_format.Width * m_format.Height / 4, m_format.Width * m_format.Height / 4);

            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(m_ptr_buffer, 0);

            this.d3dSource.Render(ptr);
        }
    }
}
