using System;
using Gtk;
using Voiceware;
using System.IO;
using System.Configuration;

public partial class MainWindow: Gtk.Window
{	
	 int nReturn = 0;
     string pText = "Hello. I am VoiceText.";
     string szServer = ConfigurationManager.AppSettings["server"];
     int nPort = 7000;
     int nSpeakerID = libttsapi.TTS_JULIE_DB;
     int nVoiceFormat = libttsapi.FORMAT_WAV;
	 int nTextFormat = libttsapi.TEXT_NORMAL;
     byte[] result = null;
     int nVoiceLen = 0;
	 string _filename = "output.wav";
	 string _path = Directory.GetParent(".")+"/audio_files";	

	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void tts_call (object sender, System.EventArgs e)
	{
		//set options
		//nTextFormat = combobox_input_format.Active;
		
		pText = textview2.Buffer.Text;
		_filename = _path+"/"+entry_filename.Text;
		
		//check if we have an audio directory
		if(!Directory.Exists(_path))
		{
			Directory.CreateDirectory(_path);			
		}	
		
		Console.WriteLine(_filename);
			
		//file_request();
		//if(combobox_input_format.Active == libttsapi.TEXT_SSML){
			tts_file();		
		//}else{
		//	tts_file_ssml();
		//}

		
	}
	
	
	private void file_request(){
		libttsapi ttsapi = new libttsapi();
		
	 // tts file request test
        nReturn = ttsapi.TTSRequestFile(szServer, nPort, pText, "", _filename, nSpeakerID, nVoiceFormat);
        if (nReturn == libttsapi.TTS_RESULT_SUCCESS)
            Console.WriteLine("RequestFile Success!!!");        
        else
            Console.WriteLine("RequestFile Failed (" + nReturn + ")!!!");
	
	}	
	
	private void tts_file(){
		
		libttsapi ttsapi = new libttsapi();
	

		// tts buffer request test
        result = ttsapi.TTSRequestBuffer(szServer, nPort, pText, out nVoiceLen, nSpeakerID, nVoiceFormat, libttsapi.TRUE, libttsapi.TRUE, out nReturn);
        if (nReturn == libttsapi.TTS_RESULT_SUCCESS)
        {
            Console.WriteLine("RequestBuffer Success!!!");
            libttsapi.WriteByteToFile(_filename, result, nVoiceLen);
        }
        else
            Console.WriteLine("RequestBuffer Failed (" + nReturn + ")!!!");
		
		

	}	
	
	private void tts_file_ssml(){
	      libttsapi a = new libttsapi(); 
          int pMarkSize = 0; 
          TTSMARK[] ppMark; 
          int nReturn = libttsapi.TTS_RESULT_CONTINUE; 
          byte[] result; 
          int nVoiceLen = 0;                          
     	  int bFirst = 1; 
          int count = 0; 
     while (nReturn == libttsapi.TTS_RESULT_CONTINUE) 
     { 
        result = a.TTSRequestBufferSSML(szServer, nPort, pText, out nVoiceLen, nSpeakerID, libttsapi.FORMAT_PCM, out pMarkSize, out ppMark, bFirst, out nReturn); 
        bFirst = 0; 
        for (int i = 0; i < pMarkSize; i++) 
        { 
        	Console.WriteLine("[" + ppMark[i].nOffsetInBuffer.ToString() + "][" + ppMark[i].nOffsetInStream.ToString() + "]" + ppMark[i].sMarkName); 
            Console.WriteLine(ppMark[i].sMarkName.Length.ToString()); 
        }
			
        if (nReturn == libttsapi.TTS_RESULT_CONTINUE || nReturn == libttsapi.TTS_RESULT_SUCCESS) 
        { 
            Console.WriteLine("RequestBufferSSML Success (length=" + nVoiceLen.ToString() + ")!!!"); 
            libttsapi.WriteByteToFile("bufferssml" + count.ToString() + ".pcm", result, nVoiceLen); 
        }else{ 
            Console.WriteLine("RequestBufferSSML Failed (" + nReturn.ToString() + ")!!!"); 
        } 
            count++; 
        }
		
		
	}	
	
	
	//Set output filename based on format//
	protected void output_format_changed (object sender, System.EventArgs e)
	{
		int _selected = combobox_output_format.Active;
		
		switch(_selected){
		case 0:
		case 1:	
			entry_filename.Text="output.wav";
			nVoiceFormat = libttsapi.FORMAT_WAV;
			break;
		case 2:
			entry_filename.Text="output_pcm.wav";
			nVoiceFormat = libttsapi.FORMAT_PCM;
			break;	
		case 3:
			entry_filename.Text="output.ulaw";
			nVoiceFormat = libttsapi.FORMAT_MULAW;
			break;	
		case 4:
			entry_filename.Text="output.alaw";
			nVoiceFormat = libttsapi.FORMAT_ALAW;
			break;	
		case 5:
			entry_filename.Text="output.adpcm";
			nVoiceFormat = libttsapi.FORMAT_32ADPCM;
			break;	
		case 6:
			entry_filename.Text="output.asf";
			nVoiceFormat = libttsapi.FORMAT_ASF;
			break;	
		case 7:
			entry_filename.Text="output.ogg";
			nVoiceFormat = libttsapi.FORMAT_OGG;
			break;	
		case 8:
			entry_filename.Text="output_8bit.wav";
			nVoiceFormat = libttsapi.FORMAT_8BITWAV;
			break;	
		case 9:
			entry_filename.Text="output_AWAV.wav";
			nVoiceFormat = libttsapi.FORMAT_AWAV;
			break;	
		case 10:
			entry_filename.Text="output_MUWAV.wav";
			nVoiceFormat = libttsapi.FORMAT_MUWAV;
			break;	
		}	
		
		
	}
	
	
	
	protected void text_format_changed (object sender, System.EventArgs e)
	{
		switch(combobox_input_format.Active){
		case 0:
			nTextFormat = libttsapi.TEXT_NORMAL;
			break;
		case 1:
			nTextFormat = libttsapi.TEXT_SSML;
			break;
		case 2:
			nTextFormat = libttsapi.TEXT_HTML;
			break;
		case 3:	
			nTextFormat = libttsapi.TEXT_EMAIL;
			break;
		}
		
		
	}

	protected void view_files (object sender, System.EventArgs e)
	{
		//audio_files _win = new audio_files();
		//	_win.show();
	}

	
}
