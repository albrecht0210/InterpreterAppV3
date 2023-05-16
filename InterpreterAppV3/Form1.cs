using InterpreterAppV3.Library.Analyze;
using System.Diagnostics;

namespace InterpreterAppV3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void run_btn_Click(object sender, EventArgs e)
        {
            Lexer lex = new Lexer(code_input.Text);
            Parser parser = new Parser(lex);
            try
            {
                parser.Parse();
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                Debug.WriteLine(err.StackTrace);
            }
        }
    }
}