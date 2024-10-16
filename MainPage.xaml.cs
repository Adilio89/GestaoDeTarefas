using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace GestaoDeTarefas;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Tarefa> Tarefas { get; set; }

    public MainPage()
    {
        InitializeComponent();
        Tarefas = new ObservableCollection<Tarefa>();
        listViewTasks.ItemsSource = Tarefas;
        dtpDueDate.Date = DateTime.Now; // Define a data inicial para hoje
    }

    private async void AddTask_Clicked(object sender, EventArgs e)
    {
        // Validação dos campos
        if (string.IsNullOrWhiteSpace(txtTarefa.Text))
        {
            await DisplayAlert("Erro", "A descrição da tarefa não pode estar vazia.", "OK");
            return;
        }

        if (cmbPriority.SelectedItem == null)
        {
            await DisplayAlert("Erro", "Por favor, selecione uma prioridade.", "OK");
            return;
        }

        var novaTarefa = new Tarefa
        {
            Descricao = txtTarefa.Text,
            Prioridade = cmbPriority.SelectedItem.ToString(),
            Vencimento = dtpDueDate.Date
        };

        Tarefas.Add(novaTarefa);
        ClearInputFields(); // Limpa os campos após adicionar a tarefa
    }

    private void RemoveTask_Clicked(object sender, EventArgs e)
    {
        var tarefaSelecionada = listViewTasks.SelectedItem as Tarefa;
        if (tarefaSelecionada != null)
        {
            Tarefas.Remove(tarefaSelecionada);
        }
        else
        {
            DisplayAlert("Erro", "Por favor, selecione uma tarefa para remover.", "OK");
        }
    }

    private async void SaveTasks_Clicked(object sender, EventArgs e)
    {
        try
        {
            var tarefasJson = JsonConvert.SerializeObject(Tarefas, Formatting.Indented);
            string path = Path.Combine(FileSystem.AppDataDirectory, "tasks.json");
            File.WriteAllText(path, tarefasJson);
            await DisplayAlert("Informação", "Tarefas salvas com sucesso!", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao salvar as tarefas: {ex.Message}", "OK");
        }
    }

    private async void LoadTasks_Clicked(object sender, EventArgs e)
    {
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, "tasks.json");
            if (File.Exists(path))
            {
                var tarefasJson = File.ReadAllText(path);
                var tarefas = JsonConvert.DeserializeObject<ObservableCollection<Tarefa>>(tarefasJson);
                Tarefas.Clear();
                foreach (var tarefa in tarefas)
                {
                    Tarefas.Add(tarefa);
                }
                await DisplayAlert("Informação", "Tarefas carregadas com sucesso!", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Nenhum arquivo de tarefas encontrado.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar as tarefas: {ex.Message}", "OK");
        }
    }

    private void ClearInputFields()
    {
        txtTarefa.Text = string.Empty;
        cmbPriority.SelectedItem = null;
        dtpDueDate.Date = DateTime.Now; // Reinicia a data para hoje
    }
}

public class Tarefa
{
    public string Descricao { get; set; }
    public string Prioridade { get; set; }
    public DateTime Vencimento { get; set; }

    public override string ToString()
    {
        return $"{Descricao} - {Prioridade} - {Vencimento:dd/MM/yyyy}";
    }
}
