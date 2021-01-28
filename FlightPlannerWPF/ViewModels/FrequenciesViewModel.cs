using FrequencyNotesLibrary;
using JsonReaderLibrary;
using Microsoft.Win32;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace FlightPlannerWPF.ViewModels
{
   public class FrequenciesViewModel : ViewModel
   {
      #region - Fields & Properties
      private string _freqFilePath;
      private string _flightPlanPath;
      private ObservableCollection<Frequency> _frequencies;
      private Frequency _selectedFreq;

      #region Commands
      public Command OpenFreqCmd { get; private set; }
      public Command SaveFreqCmd { get; private set; }
      public Command NewFreqCmd { get; private set; }
      public Command DelFreqCmd { get; private set; }
      #endregion
      #endregion

      #region - Constructors
      public FrequenciesViewModel()
      {
         Frequencies = new ObservableCollection<Frequency>();
         KneeboardViewModel.FileChanged += KneeboardViewModel_FileChanged;

         OpenFreqCmd = new Command(OpenFreq);
         SaveFreqCmd = new Command(SaveFreq);
         NewFreqCmd = new Command(NewFreq);
         DelFreqCmd = new Command(DelFreq);
      }
      #endregion

      #region - Methods
      private void KneeboardViewModel_FileChanged(object sender, string e)
      {
         FlightPlanPath = e;
      }

      private void OpenFreq(object p)
      {
         try
         {
            OpenFileDialog dialog = new OpenFileDialog
            {
               InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
               AddExtension = true,
               DefaultExt = ".frq",
               FileName = FreqFileName,
               Filter = "FREQ|*.frq|All|*.*",
               Title = "Save Frequency Notes",
               Multiselect = false,
            };

            if (dialog.ShowDialog() == true)
            {
               FreqFilePath = dialog.FileName;

               var tempFreqModel = JsonReader.OpenJsonFile<FrequencyNotesModel>(dialog.FileName);
            }
         }
         catch (Exception e)
         {
            MessageBox.Show(e.Message, "Open Freq Error");
         }
      }

      private void SaveFreq(object p)
      {
         try
         {
            SaveFileDialog dialog = new SaveFileDialog
            {
               InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
               AddExtension = true,
               DefaultExt = ".frq",
               FileName = FreqFileName,
               Filter = "FREQ|*.frq|All|*.*",
               Title = "Save Frequency Notes",
            };

            if (dialog.ShowDialog() == true)
            {
               FreqFilePath = dialog.FileName;

               FrequencyNotesModel.SaveFrequencies(dialog.FileName, new FrequencyNotesModel(dialog.FileName, Frequencies.ToList()));
               MessageBox.Show("Saved Frequencies", "Done");
            }
         }
         catch (Exception e)
         {
            MessageBox.Show(e.Message, "Save Freq Error");
         }
      }

      private void NewFreq(object p)
      {
         SelectedFreq = new Frequency();
         Frequencies.Add(SelectedFreq);
      }

      private void DelFreq(object p)
      {
         if (SelectedFreq is null) return;

         Frequencies.Remove(SelectedFreq);
         SelectedFreq = null;
      }
      #endregion

      #region - Full Properties

      public string FreqFilePath
      {
         get => _freqFilePath;
         set
         {
            _freqFilePath = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FreqFileName));
         }
      }

      public string FreqFileName
      {
         get => Path.GetFileNameWithoutExtension(FreqFilePath);
      }
      public string FlightPlanPath
      {
         get => _flightPlanPath;
         set
         {
            _flightPlanPath = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FlightPlanFileName));
         }
      }

      public string FlightPlanFileName
      {
         get
         {
            return Path.GetFileNameWithoutExtension(FlightPlanPath);
         }
      }

      public ObservableCollection<Frequency> Frequencies
      {
         get => _frequencies;
         set
         {
            _frequencies = value;
            OnPropertyChanged();
         }
      }

      public Frequency SelectedFreq
      {
         get => _selectedFreq;
         set
         {
            _selectedFreq = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
