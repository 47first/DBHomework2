namespace MyWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataGridViewDbLoad();
        }

        private void DataGridViewDbLoad()
        {
            using (AutoModelContext db = new AutoModelContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                //Если нет ни одного элемента в бд
                if (!db.Autos.Any())
                {
                    //Добавляем и сохраняем данные
                    db.Autos.Add( new AutoModel { Amount = 3, Cost = 500, Mark = "Lada", Model="Largus" } );
                    db.SaveChanges();
                }

                //Отображаем текущее состояние бд 
                dataGridView1.DataSource = db.Autos.ToList<AutoModel>();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string model = modelTb.Text;
                string mark = markTb.Text;
                int cost = int.Parse(costTb.Text);
                int amount = int.Parse(amountTb.Text);

                using (AutoModelContext db = new AutoModelContext())
                {
                    //Создаем модель для добавления в бд
                    AutoModel carToAdd = new AutoModel { Amount = amount, Cost = cost, Mark = mark, Model = model };

                    //Добавляем и сохраняем данные
                    db.Autos.Add(carToAdd);
                    db.SaveChanges();

                    //Отображаем текущее состояние бд 
                    dataGridView1.DataSource = db.Autos.ToList<AutoModel>();
                }
            }
            catch {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Попытка привести к нужному типу
                int id = int.Parse(idTb.Text);
                string model = modelTb.Text;
                string mark = markTb.Text;
                int cost = int.Parse(costTb.Text);
                int amount = int.Parse(amountTb.Text);

                using (AutoModelContext db = new AutoModelContext())
                {
                    AutoModel carToAdd = new AutoModel { Id = id, Amount = amount, Cost = cost, Mark = mark, Model = model };
                    try
                    {
                        //Пытаемся обновить данные
                        db.Autos.Update(carToAdd);
                        db.SaveChanges();

                        //Отображаем текущее состояние бд 
                        dataGridView1.DataSource = db.Autos.ToList<AutoModel>();
                    }
                    catch
                    {
                        //Отображаем сообщение об ошибке
                        MessageBox.Show("Невозможно обновить данных которых еще нет в БД");
                    }
                }
            }
            catch 
            {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(idTb.Text);

                using (AutoModelContext db = new AutoModelContext())
                {
                    //Пытаемся найти категорию на удаление из бд
                    AutoModel? categoryToRemove = db.Autos.FirstOrDefault<AutoModel?>(auto => auto.Id == id);
                    if (categoryToRemove != null)
                    {
                        //Удаляем и сохраняем
                        db.Autos.Remove(categoryToRemove);
                        db.SaveChanges();

                        //Отображаем текущее состояние бд 
                        dataGridView1.DataSource = db.Autos.ToList<AutoModel>();
                    }
                }
            }
            catch {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }
    }
}
