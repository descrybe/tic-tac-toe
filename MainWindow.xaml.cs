using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        /// <summary>
        /// Хранит текущее значение ячейки в игре
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// 1, если ход первого игрока и 0, если ход второго игрока
        /// </summary>
        private bool mPlayerTurn;

        /// <summary>
        /// 1 - игра закончена, 0 - нет
        /// </summary>
        private bool mGameEnded;
        #endregion
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();

        }
        #endregion


        /// <summary>
        /// Начало новой игры и очистка всех ячеек
        /// </summary>
        private void NewGame()
        {
            //Создание массива с 9 значениями Free
            mResults = new MarkType[9];
            for (int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;

                //Убедиться, что первый игрок начал игру
                mPlayerTurn = true;

                //Вызывает каждую кнопку на гриде
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    //Изменение фона, переднего плана и содержания на дефолтные значения
                    button.Content = string.Empty;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Blue;
                });
                //Игра не закончена
                mGameEnded = false;
            }


        }

        /// <summary>
        /// Обращается к действию нажатия кнопки
        /// </summary>
        /// <param name="sender">Кнопка, которая была нажата</param>
        /// <param name="e">Действие, произведенное нажатием</param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //начинает новую игру по клику, если предыдущая закончена 
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            //Кастует сендера к кнопке           
            var button = (Button)sender;

            //Определяет позицию кнопки в массиве
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            //Ничего не делать, если ячейка уже заполнена
            if (mResults[index] != MarkType.Free)
                return;


            //Задает значение ячейки в зависимости от того, какой игрок ходит
            /*if (mPlayerTurn)
                mResults[index] = MarkType.Cross;
            else
                mResults[index] = MarkType.Nought;  <-----можно так, но ниже более красиво, логика та же*/
            mResults[index] = mPlayerTurn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayerTurn ? "X" : "O";


            //изменяем цвет ноликов на красный
            if (mPlayerTurn)
            {
                button.Foreground = Brushes.Green;
            }
            else
            {
                button.Foreground = Brushes.Red;
            }


            mPlayerTurn ^= true;
            
            //Проверка победителя
            CheckForWinner();
            
            /// <summary>
            /// Проверяет, есть ли победитель, т.е. 3 значка подряд
            /// </summary>
            



        }

        private void CheckForWinner()
        {
            //Проверка на горизонтальные 
            #region Horizontal 0

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Blue; 
            }            
            #endregion
            #region Horizontal 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Blue;
            }
                       
            #endregion
            #region Horizontal 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Blue;
            }

            #endregion
            //Проверка на вертикальные победы
            #region Vertical 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Blue;
            }

            
            #endregion
            #region Vertical 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Blue;
            }

            
            #endregion
            #region Vertical 2
            
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Blue;
            }

            #endregion
            //Проверка на диагональные победы
            #region Diagonal 0

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Blue;
            }

            
            #endregion
            #region Diagonal 1
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                // Подсветка победных ячеек в синий
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Blue;
            }

         
            #endregion

            //Нет победителя
            if (!mResults.Any(result => result == MarkType.Free))
            {
                //игра завершена
                mGameEnded = true;

                //Изменяет цвет всех ячеек на оранжевый
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Foreground = Brushes.Gray;
                    button.Background = Brushes.Orange;

                });
            }

            if (mGameEnded)
            {
                MessageBox.Show("Игра окончена");
            }

        }
    }
}
