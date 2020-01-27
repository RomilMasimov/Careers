using System;
using System.Linq;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Models.Enums;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Careers.Services
{
    public class Initializer
    {
        private readonly CareersDbContext context;
        private readonly IClientService clientService;
        private readonly ISpecialistService specialistService;
        private readonly UserManager<AppUser> userManager;

        public Initializer(CareersDbContext context, IClientService clientService, ISpecialistService specialistService, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.clientService = clientService;
            this.specialistService = specialistService;
            this.userManager = userManager;
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

        public void Languages()
        {
            if (context.Languages.Any()) return;
            context.Languages.Add(new MyLanguage { Name = "azerbaijan" });
            context.Languages.Add(new MyLanguage { Name = "russian" });
            context.Languages.Add(new MyLanguage { Name = "english" });
            context.Languages.Add(new MyLanguage { Name = "arabic" });
            context.SaveChanges();
        }

        public void CategorySubCategory()
        {
            if (context.SubCategories.Any()) return;
            var repetitor = context.Categories.Add(new Category { DescriptionRU = "репетиторы", DescriptionAZ = "Müəllimlər" });
            var doctor = context.Categories.Add(new Category { DescriptionRU = "врачи", DescriptionAZ = "Həkimlər" });
            var repair = context.Categories.Add(new Category { DescriptionRU = "мастера по ремонту", DescriptionAZ = "Ustalar" });
            var coach = context.Categories.Add(new Category { DescriptionRU = "спортивные тренеры", DescriptionAZ = "İdman" });
            var it = context.Categories.Add(new Category { DescriptionRU = "ит-фрилансеры", DescriptionAZ = "IT" });
            var other = context.Categories.Add(new Category { DescriptionRU = "другое", DescriptionAZ = "Digər" });

            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "İnglis dili", DescriptionRU = "Английский язык" });
            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "Biologiya", DescriptionRU = "Биология" });
            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "Mühasibat uçotu", DescriptionRU = "Бухгалтерский учёт" });
            context.SubCategories.Add(new SubCategory { Category = repetitor.Entity, DescriptionAZ = "Dizayn", DescriptionRU = "Дизайн" });

            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "Allergologiya", DescriptionRU = "Аллергология" });
            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "Mamalıq", DescriptionRU = "Акушерство" });
            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "Androloji", DescriptionRU = "Андрология" });
            context.SubCategories.Add(new SubCategory { Category = doctor.Entity, DescriptionAZ = "Venereologiya", DescriptionRU = "Венерология" });

            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "Ətrafın abadlaşdırılması", DescriptionRU = "Благоустройство территории " });
            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "Quyu qazma", DescriptionRU = "Бурение скважин " });
            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "Məişət texnikası", DescriptionRU = "Бытовая техника " });
            context.SubCategories.Add(new SubCategory { Category = repair.Entity, DescriptionAZ = "Havalandırma və kondisioner", DescriptionRU = "Вентиляция и кондиционеры " });

            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "İdman salonu icarəsi", DescriptionRU = "Аренда спортзалов" });
            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "Döyüş sənəti", DescriptionRU = "Единоборства" });
            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "Oyun idman növləri", DescriptionRU = "Игровые виды спорта" });
            context.SubCategories.Add(new SubCategory { Category = coach.Entity, DescriptionAZ = "Fərdi idman növləri", DescriptionRU = "Индивидуальные виды спорта" });

            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "Dizaynerler", DescriptionRU = "Дизайнеры" });
            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "Mətnlərlə işləmək", DescriptionRU = "Работа с текстами" });
            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "Proqramçılar", DescriptionRU = "Программисты" });
            context.SubCategories.Add(new SubCategory { Category = it.Entity, DescriptionAZ = "Tərcüməçilər", DescriptionRU = "Переводчики" });
            
            context.SaveChanges();
        }

        public void Services()
        {
            if (context.Services.Any()) return;
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "Kitab tərcüməsi", DescriptionRU = "Перевод книг" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "İngilis dilindən tərcümə", DescriptionRU = "Перевод с английского" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "Koreya dilindən tərcümə", DescriptionRU = "Перевод с корейского" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Переводчики"), DescriptionAZ = "Fransız dilindən tərcümə", DescriptionRU = "Перевод с французского" });

            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "Uyğunlaşma ardıcıllığı", DescriptionRU = "Адаптивная вёрстка" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "Elan lövhəsi", DescriptionRU = "Доска объявлений" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "1C proqram təminatının dəyişdirilməsi", DescriptionRU = "Настройка 1С Розницы" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "Sayt yaradılması (PHP)", DescriptionRU = "Создание сайта на PHP" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Программисты"), DescriptionAZ = "Sayt yaradılması (uCoz)", DescriptionRU = "Создание сайта на uCoz" });

            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "Kataloqların dizaynı", DescriptionRU = "Дизайн каталогов" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "Menyu dizaynı", DescriptionRU = "Дизайн меню" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "Plakat dizaynı", DescriptionRU = "Дизайн плакатов" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "Bina modelləşdirilməsi", DescriptionRU = "Моделирование здания" });
            context.Services.Add(new Service { SubCategory = context.SubCategories.FirstOrDefault(x => x.DescriptionRU == "Дизайнеры"), DescriptionAZ = "Mokap tərtibi", DescriptionRU = "Разработка мокапа" });

            context.SaveChanges();
        }

        public void MeetingPoints()
        {
            if (context.MeetingPoints.Any()) return;
            context.MeetingPoints.Add(new MeetingPoint { Description = "SubWay 1", MeetingPointType = MeetingPointTypeEnum.Subway, CityId = 1 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "SubWay 2", MeetingPointType = MeetingPointTypeEnum.Subway, CityId = 2 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "SubWay 2", MeetingPointType = MeetingPointTypeEnum.Subway, CityId = 3 });

            context.MeetingPoints.Add(new MeetingPoint { Description = "City 1", MeetingPointType = MeetingPointTypeEnum.City, CityId = 1 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "City 2", MeetingPointType = MeetingPointTypeEnum.City, CityId = 2 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "City 3", MeetingPointType = MeetingPointTypeEnum.City, CityId = 3 });

            context.MeetingPoints.Add(new MeetingPoint { Description = "District 1", MeetingPointType = MeetingPointTypeEnum.District, CityId = 1 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "District 2", MeetingPointType = MeetingPointTypeEnum.District, CityId = 2 });
            context.MeetingPoints.Add(new MeetingPoint { Description = "District 3", MeetingPointType = MeetingPointTypeEnum.District, CityId = 3 });
            context.SaveChanges();
        }

        public void QuestionAndAnswers()
        {
            if (context.Questions.Any()) return;

            #region Программисты
            var prog = context.SubCategories.FirstOrDefault(x => x.DescriptionRU.ToLower() == "программисты");
            var p1 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Что необходимо разработать?", TextAZ = "Nə proqramı yazılmalıdır ?", SubCategory = prog });
            var p2 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Для чего?", TextAZ = "Nə üçün ?", SubCategory = prog });
            var p3 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Single, TextRU = "Язык программирования?", TextAZ = "Proqramlaşdırma dili ?", SubCategory = prog });
            var p4 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Детали и сроки задачи?", TextAZ = "Son tarix ?", SubCategory = prog });
            var p6 = context.Questions.Add(new Question { Type = QuestionTypeEnum.MyLocation, TextRU = "Ваш адрес", TextAZ = "Sizin ünvanınız", SubCategory = prog });
            var p7 = context.Questions.Add(new Question { Type = QuestionTypeEnum.MeetingPoints, TextRU = "Куда вам удобно приехать?", TextAZ = "Sizə hara gəlmək rahatdı ?", SubCategory = prog });

            context.SaveChanges();

            context.Answers.Add(new Answer { TextRU = "JavaScript", TextAZ = "JavaScript", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Java", TextAZ = "Java", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Python", TextAZ = "Python", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "PHP", TextAZ = "PHP", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "C++", TextAZ = "C++", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "CSS", TextAZ = "CSS", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "C#", TextAZ = "C#", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "C", TextAZ = "C", QuestionId = p3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Не знаю, нужна рекомендация специалиста", TextAZ = "Bilmirəm, mütəxəssis tövsiyəsinə ehtiyacım var", QuestionId = p3.Entity.Id });
            #endregion

            var php = context.Services.FirstOrDefault(x => x.DescriptionRU.ToLower() == "Создание сайта на PHP");
            var phpq1 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Single, TextRU = "Тип спайта", TextAZ = "Sayt növü", SubCategoryId = php.SubCategoryId, ServiceId = php.Id });
            var phpq2 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Single, TextRU = "Тип вёрстки", TextAZ = "Layout növü", SubCategoryId = php.SubCategoryId, ServiceId = php.Id });

            context.SaveChanges();

            context.Answers.Add(new Answer { TextRU = "Интернет сайт", TextAZ = "Onlayn mağaza", QuestionId = phpq1.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Сайт-визитка", TextAZ = "Vizit kart saytı", QuestionId = phpq1.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Корпоративный сайт", TextAZ = "Korporativ veb sayt", QuestionId = phpq1.Entity.Id });

            context.Answers.Add(new Answer { TextRU = "Адаптивная", TextAZ = "Uyğunlaşır", QuestionId = phpq2.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Фиксированная", TextAZ = "Sabitdir", QuestionId = phpq2.Entity.Id });


            #region Переводчики
            var translators = context.SubCategories.FirstOrDefault(x => x.DescriptionRU.ToLower() == "переводчики");
            var t1 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Multi, TextRU = "Перевод", TextAZ = "Tərcümə", SubCategory = translators });
            var t2 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Multi, TextRU = "Язык, с которого нужно перевести:", TextAZ = "Mənbə dili:", SubCategory = translators });
            var t3 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Multi, TextRU = "На какой язык перевести?", TextAZ = "Hansı dilə tərcümə edim?", SubCategory = translators });
            var t4 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Single, TextRU = "Что перевести?", TextAZ = "Nə tərcümə etmək lazımdır?", SubCategory = translators });
            var t5 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Объём работ", TextAZ = "İşin həcmi", SubCategory = translators });
            var t6 = context.Questions.Add(new Question { Type = QuestionTypeEnum.Input, TextRU = "Опишите детали задачи", TextAZ = "Proekt haqqında incəlikləri qeyd edin", SubCategory = translators });

            context.SaveChanges();

            context.Answers.Add(new Answer { TextRU = "Устный", TextAZ = "Şifahi", QuestionId = t1.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Письменный", TextAZ = "Yazılı", QuestionId = t1.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Английский", TextAZ = "İngilis", QuestionId = t2.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Русский", TextAZ = "Rus", QuestionId = t2.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Азербайджанский", TextAZ = "Azərbaycan", QuestionId = t2.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Английский", TextAZ = "İngilis", QuestionId = t3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Русский", TextAZ = "Rus", QuestionId = t3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Азербайджанский", TextAZ = "Azərbaycan", QuestionId = t3.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Техническую документацию", TextAZ = "Texniki sənədlər", QuestionId = t4.Entity.Id });
            context.Answers.Add(new Answer { TextRU = "Художественный текст", TextAZ = "Bədii mətn", QuestionId = t4.Entity.Id });
            #endregion

            context.SaveChanges();
        }

        public async Task ClientsAndSpecialistsAsync()
        {
            var password = "$Secret123";

            var userClient1 = new AppUser { Email = "clientemail1@gmail.com", PhoneNumber = "0000000000", UserName = "clientemail1@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            var userClient2 = new AppUser { Email = "clientemail2@gmail.com", PhoneNumber = "0000000000", UserName = "clientemail2@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true };

            await userManager.CreateAsync(userClient1, password);
            await userManager.AddToRoleAsync(userClient1, "client");
            await userManager.CreateAsync(userClient2, password);
            await userManager.AddToRoleAsync(userClient2, "client");

            var client1 = new Client { Name = "ClientName1", Surname = "ClientSurname1", Gender = true, AppUserId = userClient1.Id };
            var client2 = new Client { Name = "ClientName2", Surname = "ClientSurname2", Gender = false, AppUserId = userClient2.Id };

            await clientService.InsertAsync(client1);
            await clientService.InsertAsync(client2);

            var userSpecialist1 = new AppUser { Email = "specialistemail1@gmail.com", PhoneNumber = "0000000000", UserName = "specialistemail1@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            var userSpecialist2 = new AppUser { Email = "specialistemail2@gmail.com", PhoneNumber = "0000000000", UserName = "specialistemail2@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true };

            await userManager.CreateAsync(userSpecialist1, password);
            await userManager.AddToRoleAsync(userSpecialist1, "specialist");
            await userManager.CreateAsync(userSpecialist2, password);
            await userManager.AddToRoleAsync(userSpecialist2, "specialist");

            var specialist1 = new Specialist { Name = "SpecName1", Surname = "SpecSurname1", Gender = true, Fathername = "SpecFathername1", CityId = 1, DateOfBirth = DateTime.Now, AppUserId = userSpecialist1.Id };
            var specialist2 = new Specialist { Name = "SpecName2", Surname = "SpecSurname2", Gender = false, Fathername = "SpecFathername2", CityId = 2, DateOfBirth = DateTime.Now, AppUserId = userSpecialist2.Id };

            await specialistService.InsertAsync(specialist1);
            await specialistService.InsertAsync(specialist2);
        }

        public void Measurements()
        {
            if (context.Measurements.Any()) return;
            context.Measurements.Add(new Measurement { TextAZ = "dənə", TextRU = "шт" });
            context.Measurements.Add(new Measurement { TextAZ = "30 dəqiqə", TextRU = "30 мин" });
            context.Measurements.Add(new Measurement { TextAZ = "1 saat", TextRU = "1 час" });
            context.Measurements.Add(new Measurement { TextAZ = "Dərs", TextRU = "урок" });
            context.Measurements.Add(new Measurement { TextAZ = "Xidmət", TextRU = "услуга" });
            context.SaveChanges();
        }
    }
}
