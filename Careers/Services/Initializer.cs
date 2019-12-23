using System.Linq;
using Careers.EF;
using Careers.Models;
using Careers.Models.Enums;

namespace Careers.Services
{
    public class Initializer
    {
        private readonly CareersDbContext context;

        public Initializer(CareersDbContext context)
        {
            this.context = context;
        }

        public void CountryAndCity()
        {
            if (context.Cities.Any()) return;

            var country = context.Countries.Add(new Country { Name = "azerbaijan" });
            context.Cities.Add(new City { Country = country.Entity, Name = "Baku" });
            context.Cities.Add(new City { Country = country.Entity, Name = "sumqayit" });
            context.Cities.Add(new City { Country = country.Entity, Name = "ganja" });
            context.Cities.Add(new City { Country = country.Entity, Name = "lankaran" });
            context.Cities.Add(new City { Country = country.Entity, Name = "mingachevir" });
            context.Cities.Add(new City { Country = country.Entity, Name = "nakhchivan" });
            context.Cities.Add(new City { Country = country.Entity, Name = "shirvan" });
            context.Cities.Add(new City { Country = country.Entity, Name = "shaki" });
            context.Cities.Add(new City { Country = country.Entity, Name = "yevlakh" });
            context.Cities.Add(new City { Country = country.Entity, Name = "khankendi" });
            context.SaveChanges();
        }

        public void CategorySubCategory()
        {
            if (context.SubCategories.Any()) return;
            var repetitor = context.Categories.Add(new Category { DescriptionRU = "репетиторы", DescriptionAZ = "muəllimlər" });
            var doctor = context.Categories.Add(new Category { DescriptionRU = "врачи", DescriptionAZ = "həkimlər" });
            var repair = context.Categories.Add(new Category { DescriptionRU = "мастера по ремонту", DescriptionAZ = "ustalar" });
            var coach = context.Categories.Add(new Category { DescriptionRU = "спортивные тренеры", DescriptionAZ = "idman müəllimlər" });
            var it = context.Categories.Add(new Category { DescriptionRU = "ит-фрилансеры", DescriptionAZ = "it-frilanserər" });

            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "inglis dili", DescriptionRU = "Английский язык" });
            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "biologiya", DescriptionRU = "Биология" });
            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "buqalteriya", DescriptionRU = "Бухгалтерский учёт" });
            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "dizayn", DescriptionRU = "Дизайн" });

            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "allerkologiya", DescriptionRU = "Аллергология" });
            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "akusher", DescriptionRU = "Акушерство" });
            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "andrologiya", DescriptionRU = "Андрология" });
            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "venerologiya", DescriptionRU = "Венерология" });

            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "territoriya ve nese", DescriptionRU = "Благоустройство территории " });
            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "skvajina drelliyen", DescriptionRU = "Бурение скважин " });
            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "bitovaya texnika", DescriptionRU = "Бытовая техника " });
            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "ventilyaciya", DescriptionRU = "Вентиляция и кондиционеры " });

            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "arenda sgorta", DescriptionRU = "Аренда спортзалов" });
            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "restling", DescriptionRU = "Единоборства" });
            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "oyun idmani", DescriptionRU = "Игровые виды спорта" });
            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "individual idmanlar", DescriptionRU = "Индивидуальные виды спорта" });

            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "dizaynerler", DescriptionRU = "Дизайнеры" });
            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "is testnen", DescriptionRU = "Работа с текстами" });
            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "proqramci", DescriptionRU = "Программисты" });
            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "tercumeci", DescriptionRU = "Переводчики" });

            context.SaveChanges();
        }

        public void Services()
        {
            if (context.Services.Any()) return;
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "sdfsdfsdf", DescriptionRU = "Перевод книг" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "sfsdfsdg", DescriptionRU = "Перевод с английского" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "ASasA", DescriptionRU = "Перевод с корейского" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "ADFSHNG", DescriptionRU = "Перевод с французского" });

            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "WEGRZHTJYHTFDGFGD", DescriptionRU = "Адаптивная вёрстка" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "ASDFDGFGNFF", DescriptionRU = "Доска объявлений" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "fsgdhfj", DescriptionRU = "Настройка 1С Розницы" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "adfsgdfd", DescriptionRU = "Создание сайта на PHP" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "asFSDzgdhf", DescriptionRU = "Создание сайта на uCoz" });

            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "fxtdygchkj", DescriptionRU = "Дизайн каталогов" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "dszsgsfgh", DescriptionRU = "Дизайн меню" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "fghfchj", DescriptionRU = "Дизайн плакатов" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "zdgxhfcjghkvjg", DescriptionRU = "Моделирование здания" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "zgxhtcyjguk zxdheg", DescriptionRU = "Разработка мокапа" });
            context.SaveChanges();
        }

        public void MeetingPoints()
        {
            if (context.Services.Any()) return;
            context.MeetingPoints.Add(new MeetingPoint { Description = "SubWay 1", MeetingPointType = MeetingPointTypeEnum.Subway, CityId = 1 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "SubWay 1", MeetingPointType = MeetingPointTypeEnum.Subway, CityId = 2 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "SubWay 2", MeetingPointType = MeetingPointTypeEnum.Subway, CityId = 3 });

            context.MeetingPoints.Add(new MeetingPoint { Description = "City 2", MeetingPointType = MeetingPointTypeEnum.City, CityId = 1 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "City 2", MeetingPointType = MeetingPointTypeEnum.City, CityId = 2 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "City 3", MeetingPointType = MeetingPointTypeEnum.City, CityId = 3 });

            context.MeetingPoints.Add(new MeetingPoint { Description = "District 3", MeetingPointType = MeetingPointTypeEnum.District, CityId = 1 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "District 3", MeetingPointType = MeetingPointTypeEnum.District, CityId = 2 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "District 3", MeetingPointType = MeetingPointTypeEnum.District, CityId = 3 });
            context.SaveChanges();
        }

        public void QuestionAndAnswers()
        {
            var subcategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU.ToLower() == "программисты");
            if (context.Questions.Any()) return;
            var q1 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Что необходимо разработать?", TextAZ = "neynemek lazimdir?", SubCategory = subcategory });
            var q2 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Для чего?", TextAZ = "Ne ucun?", SubCategory = subcategory });
            var q3 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Single, TextRU = "Язык программирования?", TextAZ = "Hansi dilinde?", SubCategory = subcategory });
            var q4 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Детали и сроки задачи?", TextAZ = "deadline?", SubCategory = subcategory });
            var q5 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Single, TextRU = "Когда приступить к задаче?", TextAZ = "Ne vaxt bashlayaq?", SubCategory = subcategory });
            var q6 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Multi, TextRU = "Подходящее время", TextAZ = "uygun saat?", SubCategory = subcategory });
            var def = context.Questions.Add(new Question { Type = QuestionTypeEnum.Multi, TextRU = "Остались пожелания к заказу?", TextAZ = "Artiq nese yazmaq isteyerdiz", SubCategory = subcategory });

            context.Answers.Add(new Answer { TextRU = "", TextAZ = "", AskedQuestion = q1.Entity, NextQuestion = q2.Entity });

            context.Answers.Add(new Answer { TextRU = "", TextAZ = "", AskedQuestion = q2.Entity, NextQuestion = q3.Entity });

            context.Answers.Add(new Answer { TextRU = "Java", TextAZ = "Java", AskedQuestion = q3.Entity, NextQuestion = q4.Entity });
            context.Answers.Add(new Answer { TextRU = "JavaScript", TextAZ = "JavaScript", AskedQuestion = q3.Entity, NextQuestion = q4.Entity });
            context.Answers.Add(new Answer { TextRU = "C#", TextAZ = "C#", AskedQuestion = q3.Entity, NextQuestion = q4.Entity });
            context.Answers.Add(new Answer { TextRU = "PHP", TextAZ = "PHP", AskedQuestion = q3.Entity, NextQuestion = q4.Entity });

            context.Answers.Add(new Answer { TextRU = "", TextAZ = "", AskedQuestion = q4.Entity, NextQuestion = q5.Entity });

            context.Answers.Add(new Answer { TextRU = "Завтра", TextAZ = "sabax", AskedQuestion = q5.Entity, NextQuestion = q6.Entity });
            context.Answers.Add(new Answer { TextRU = "Послезавтра", TextAZ = "", AskedQuestion = q5.Entity, NextQuestion = q6.Entity });
            context.Answers.Add(new Answer { TextRU = "Пусть специалист предложит время", TextAZ = "", AskedQuestion = q5.Entity, NextQuestion = q6.Entity });

            context.Answers.Add(new Answer { TextRU = "15:00 — 18:00", TextAZ = "15:00 — 18:00", AskedQuestion = q6.Entity, });
            context.Answers.Add(new Answer { TextRU = "18:00 — 21:00", TextAZ = "18:00 — 21:00", AskedQuestion = q6.Entity,  });
            context.Answers.Add(new Answer { TextRU = "После 21:00", TextAZ = "После 21:00", AskedQuestion = q6.Entity,  });

            context.Answers.Add(new Answer { TextRU = "", TextAZ = "", AskedQuestion = def.Entity });

            context.SaveChanges();
        }

    }
}
