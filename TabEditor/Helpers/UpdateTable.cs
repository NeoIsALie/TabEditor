using System;
using System.Linq;
using System.Data.Linq;
using System.IO;
using System.Collections.Generic;
using MusicXml;
using MusicXml.Domain;
using System.Globalization;
using System.Text;
using System.Xml;
using Encoding = MusicXml.Domain.Encoding;
using ABC;
using ABC.Domain;
using System.Security.Cryptography;

namespace DBCourse
{
    public class DataTables
    {
        private String connectionString = @"Data Source = DESKTOP-F796EKR\SQLEXPRESS; Database = PrtsDB; Integrated Security = true";
        private static PrtsDataContext db;
        private Table<partiture> partituresTable;
        private Table<creators> creatorsTable;
        private Table<instruments> instrumentsTable;
        private Table<users> usersTable;
        private Table<software> softwareTable;
        private Table<addedby> addedTable;

        public DataTables() { }

        public void SetTables()
        {
            if (db != null)
                db.Dispose();
            db = new PrtsDataContext(connectionString);
            partituresTable = db.GetTable<partiture>();
            creatorsTable = db.GetTable<creators>();
            instrumentsTable = db.GetTable<instruments>();
            softwareTable = db.GetTable<software>();
            usersTable = db.GetTable<users>();
            addedTable = db.GetTable<addedby>();
        }


        public void InsertUser(String name, String login, String password, DateTime date)
        {
            users user = new users();
            user.pk_user_id = usersTable.Count<users>() + 1;
            user.full_name = name;
            user.user_login = login;
            user.user_password = CreateMD5(password);
            user.registered_date = date;
            usersTable.InsertOnSubmit(user);
            db.SubmitChanges();
        }


        public void insertNewPartitureXml(string path, string login, int originalVersionId)
        {
            MusicXmlParser parser = new MusicXmlParser();
            Score newScore = parser.GetScore(path);
            partiture partiture = new partiture
            {
                pk_partiture_id = partituresTable.Count() + 1,
                work_number = newScore.MovementNumber.ToString(),
                title = newScore.MovementTitle,
                tempo = newScore.Parts.FirstOrDefault<Part>().Measures.FirstOrDefault<Measure>().Attributes.Divisions.ToString(),
                meter = newScore.Parts.FirstOrDefault<Part>().Measures.FirstOrDefault<Measure>().Attributes.Time.Beats.ToString() + "/" + newScore.Parts.FirstOrDefault<Part>().Measures.FirstOrDefault<Measure>().Attributes.Time.Mode,
                note_length = "1/4",
                part_key = keyFromFifths(newScore.Parts.FirstOrDefault<Part>().Measures.FirstOrDefault().Attributes.Key.Fifths),
                filepath = path
            };
            if (originalVersionId == 0)
                partiture.version_of = partituresTable.Count() + 1;
            else
                partiture.version_of = originalVersionId;

            creators creators = new creators();
            creators.pk_creators_id = creatorsTable.Count() + 1;
            creators.composer = newScore.Identification.Composer;
            creators.poet = (newScore.Identification.Poet != string.Empty) ? newScore.Identification.Poet : newScore.Identification.Lyricist;
            System.Diagnostics.Debug.Print(creators.composer + creators.poet);
            creators.authors_of = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1;

            if (newScore.Instrument != null)
            {
                if (newScore.Instrument.Name != string.Empty)
                {
                    instruments instrument = new instruments
                    {
                        pk_instrument_id = instrumentsTable.Count() + 1,
                        instrument_name = newScore.Instrument.Name,
                        fk_part_in = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1 
                    };
                    instrumentsTable.InsertOnSubmit(instrument);
                }
            }
            else
            {
                instruments instrument = new instruments
                {
                    pk_instrument_id = instrumentsTable.Count() + 1,
                    instrument_name = string.Empty,
                    fk_part_in = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1
                };
                instrumentsTable.InsertOnSubmit(instrument);
            }

            software soft = new software
            {
                pk_software_id = softwareTable.Count() + 1,
                software_name = newScore.Identification.Encoding.Software,
                encoding_date = newScore.Identification.Encoding.EncodingDate,
                fk_used_for_encoding = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1
            };

            addedby added = new addedby
            {
                id = addedTable.Count() + 1,
                fk_user_id = GetUserIdByLogin(login),
                fk_partiture_id = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1,
                added_on = DateTime.Now
            };

            partituresTable.InsertOnSubmit(partiture);
            creatorsTable.InsertOnSubmit(creators);
            softwareTable.InsertOnSubmit(soft);
            addedTable.InsertOnSubmit(added);

            db.SubmitChanges();
        }

        public void insertNewPartitureABC(string path, string login, int originalVersionId)
        {
            ABCParser parser = new ABCParser();
            ABCScore newScore = parser.GetABCScore(path);
            partiture partiture = new partiture
            {
                    pk_partiture_id = partituresTable.Count() + 1,
                    work_number = newScore.Header.Reference.ToString(),
                    title = newScore.Header.Titles.FirstOrDefault<string>(),
                    tempo = newScore.Header.Tempo,
                    meter = newScore.Header.Meter,
                    note_length = newScore.Header.NoteLength,
                    part_key = newScore.Header.Key,
                    filepath = path,
                    version_of = (originalVersionId > 0) ? originalVersionId : partituresTable.Count() + 1
            };
            if(newScore.Header.Composers.Count() > 0)
            { 
                creators creators = new creators
                {
                    pk_creators_id = creatorsTable.Count() + 1,
                    composer = newScore.Header.Composers.FirstOrDefault(),
                    poet = newScore.Header.Composers.FirstOrDefault(),
                    authors_of = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1
                };
                creatorsTable.InsertOnSubmit(creators);
            }
            instruments instrument = new instruments
            {
                pk_instrument_id = instrumentsTable.Count() + 1,
                instrument_name = newScore.Header.Instrument.ToString(),
                fk_part_in = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1
            };
            instrumentsTable.InsertOnSubmit(instrument);

            software soft = new software
            {
                pk_software_id = softwareTable.Count() + 1,
                software_name = newScore.Header.Encoding.Software,
                encoding_date = newScore.Header.Encoding.EncodingDate,
                fk_used_for_encoding = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1
            };

            addedby added = new addedby
            {
                id = addedTable.Count() + 1,
                fk_user_id = GetUserIdByLogin(login),
                fk_partiture_id = (partituresTable.Count() > 0) ? partituresTable.Count() + 1 : 1,
                added_on = DateTime.Now
            };

            partituresTable.InsertOnSubmit(partiture);
            softwareTable.InsertOnSubmit(soft);
            addedTable.InsertOnSubmit(added);
            db.SubmitChanges();
        }

        /*
        public void updatePartitureInfoXml(string path)
        {
            MusicXmlParser parser = new MusicXmlParser();
            Score score = parser.GetScore(path);
            IQueryable<partiture> partituresToUpdate = GetPartitures(path);

            foreach(partiture p in partituresToUpdate)
            {
                p.work_number = score.Work.Number;
                p.title = score.Work.Title;
                p.tempo = score.Parts.FirstOrDefault<Part>().Tempo.ToString();
                foreach (Part part in score.Parts)
                {
                    foreach (Measure measure in part.Measures)
                    {
                        if (measure.Attributes.Key != null)
                        {
                            p.part_key = keyFromFifths(measure.Attributes.Key.Fifths);
                            break;
                        }
                    }
                }

                IQueryable<creators> authors = GetPartitureAuthors(p.pk_partiture_id);
                if (authors != null)
                {
                    foreach (creators author in authors)
                    {
                        author.composer = score.Identification.Composer;
                        author.poet = score.Identification.Poet;
                    }
                }

                IQueryable<software> soft = GetPartitureEncoding(p.pk_partiture_id);
                foreach (software s in soft)
                {
                    s.software_name = score.Identification.Encoding.Software;
                    s.encoding_date = score.Identification.Encoding.EncodingDate;
                }

                IQueryable<instruments> instrumentsToUpdate = GetPartitureInstruments(p.pk_partiture_id);
                foreach(instruments i in instrumentsToUpdate)
                {
                    i.instrument_name = score.Instrument.Name;
                }
            }

            db.SubmitChanges();
        }

        public void updatePartitureInfoABC(string path)
        {
            ABCParser parser = new ABCParser();
            ABCScore score = parser.GetABCScore(path);
            IQueryable<partiture> partituresToUpdate = GetPartitures(path);
            foreach(partiture p in partituresToUpdate)
            {
                p.work_number = score.Header.Reference.ToString();
                p.title = score.Header.Titles.FirstOrDefault<string>();
                p.tempo = score.Header.Tempo;
                p.meter = score.Header.Meter;
                p.note_length = score.Header.NoteLength;
                p.part_key = score.Header.Key;

                IQueryable<creators> authors = GetPartitureAuthors(p.pk_partiture_id);
                foreach (creators author in authors)
                {
                    author.composer = score.Header.Composers.FirstOrDefault<string>();
                    author.poet = score.Header.Composers.FirstOrDefault<string>();
                }

                IQueryable<instruments> instrument = GetPartitureInstruments(p.pk_partiture_id);
                foreach (instruments ins in instrument)
                {
                    ins.instrument_name = score.Header.Instrument;
                }

            }
            db.SubmitChanges();
        }
        */

        public String keyFromFifths(int fifths)
        {
            String key = "C";
            switch (fifths)
            {
                case -7:
                    {
                        key = "Cb";
                        break;
                    }
                case -6:
                    {
                        key = "Gb";
                        break;
                    }
                case -5:
                    {
                        key = "Db";
                        break;
                    }
                case -4:
                    {
                        key = "Ab";
                        break;
                    }
                case -3:
                    {
                        key = "Eb";
                        break;
                    }
                case -2:
                    {
                        key = "Bb";
                        break;
                    }
                case -1:
                    {
                        key = "F";
                        break;
                    }
                case 0:
                    {
                        key = "C";
                        break;
                    }
                case 1:
                    {
                        key = "G";
                        break;
                    }
                case 2:
                    {
                        key = "D";
                        break;
                    }
                case 3:
                    {
                        key = "A";
                        break;
                    }
                case 4:
                    {
                        key = "E";
                        break;
                    }
                case 5:
                    {
                        key = "B";
                        break;
                    }
                case 6:
                    {
                        key = "F#";
                        break;
                    }
                case 7:
                    {
                        key = "C#";
                        break;
                    }
            }
            return key;
        }
        /*
        public IQueryable<partiture> GetPartitures(String path)
        {
            IQueryable<partiture> partitures = from p in partituresTable
                                                       where p.filepath == path
                                                       select p;
            return partitures;
        } 

        public IQueryable<creators> GetPartitureAuthors(int Id)
        {
            IQueryable<creators> authors = from auth in creatorsTable
                                           where auth.authors_of == Id
                                           select auth;
            return authors;
        }

        public IQueryable<software> GetPartitureEncoding(int Id)
        {
            IQueryable<software> encoding = from s in softwareTable
                                            where s.pk_software_id == Id
                                            select s;
            return encoding;
        }

        public IQueryable<instruments> GetPartitureInstruments(int Id)
        {
            IQueryable<instruments> instrument = from i in instrumentsTable
                                                 where i.fk_part_in == Id
                                                 select i;

            return instrument;
        }
        */

        public int GetPartitureOriginalId(string path)
        {
            IQueryable<partiture> partitures = from p in partituresTable
                                               where p.filepath == path
                                               select p;

            return partitures.Single<partiture>().version_of;
        }
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private IQueryable<users> GetUsersAuthorized(string login, string password)
        {
            IQueryable<users> us = from u in usersTable
                                   where u.user_login.Trim() == login && u.user_password.Trim() == CreateMD5(password)
                                   select u;
            return us;
        }
        
        private int GetUserIdByLogin(String login)
        {
            IQueryable<users> user = from u in usersTable
                                     where u.user_login.Trim() == login
                                     select u;
            return user.Single().pk_user_id;
        }

        public users CheckAuthorization(String login, String password)
        {
            IQueryable<users> check = GetUsersAuthorized(login, password);
            users sc = null;
            if (check.Count<users>() != 0)
                sc = check.Single<users>();
            return sc;
        }

        public Boolean CheckIfExists(String login)
        {
            Boolean exist = false;
            IQueryable<users> check = from s in usersTable
                                           where s.user_login.Trim() == login
                                           select s;
            if (check.Count<users>() != 0)
                exist = true;
            return exist;
        }

        public List<string> LoadFiles()
        {
            List<string> files = new List<string>();

            var versionGroups = from p in partituresTable
                                group p by p.version_of
                      into g
                                select new
                                {
                                    original = g.Key,
                                    versions = from p in g
                                               orderby p.pk_partiture_id
                                               select p                                      
                                };

            foreach(var group in versionGroups)
            {
                files.Add(group.versions.Last().filepath);
            }

            return files;
        }

        public void deleteFromDatabase(String filepath)
        {
            IEnumerable<partiture> res = from p in partituresTable
                      where p.filepath == filepath
                      select p;

            IEnumerable<instruments> ids = (from i in instrumentsTable
                                            join p in partituresTable on i.fk_part_in equals p.pk_partiture_id
                                            where p.filepath == filepath
                                            select i).ToList();

            IEnumerable<software> soft = (from s in softwareTable
                                        join p in partituresTable on s.fk_used_for_encoding equals p.pk_partiture_id
                                        where p.filepath == filepath
                                        select s).ToList();

            IEnumerable<creators> c = (from auth in creatorsTable
                                     join p in partituresTable on auth.authors_of equals p.pk_partiture_id
                                     where p.filepath == filepath
                                     select auth).ToList();

            IEnumerable<addedby> add = (from a in addedTable
                                      join p in partituresTable on a.fk_partiture_id equals p.pk_partiture_id
                                      where p.filepath == filepath
                                      select a).ToList();


            partituresTable.DeleteAllOnSubmit(res);
            instrumentsTable.DeleteAllOnSubmit(ids);
            softwareTable.DeleteAllOnSubmit(soft);
            creatorsTable.DeleteAllOnSubmit(c);
            addedTable.DeleteAllOnSubmit(add);

            db.SubmitChanges();
        }

        IQueryable<partiture> getVersions(int originalId)
        {
            IQueryable<partiture> versions = from p in partituresTable
                                             join c in creatorsTable on p.pk_partiture_id equals c.authors_of
                                             join i in instrumentsTable on p.pk_partiture_id equals i.fk_part_in
                                             join s in softwareTable on p.pk_partiture_id equals s.fk_used_for_encoding
                                             join a in addedTable on p.pk_partiture_id equals a.fk_partiture_id
                                             join u in usersTable on a.fk_user_id equals u.pk_user_id
                                             where p.version_of == originalId
                                             orderby p.pk_partiture_id
                                             select p;

            return versions;
        }

        public List<string> getVersionsInfo(int originalId)
        {
            string info = string.Empty;
            string partitureInfo = string.Empty;
            string creatorsInfo = string.Empty;
            string softwareInfo = string.Empty;
            string userInfo = string.Empty;

            List<string> versionInfo = new List<string>();
            IQueryable<partiture> versions = getVersions(originalId);
            /*
            IQueryable<partiture> versions = from p in partituresTable
                           join c in creatorsTable on p.pk_partiture_id equals c.authors_of
                           join i in instrumentsTable on p.pk_partiture_id equals i.fk_part_in
                           join s in softwareTable on p.pk_partiture_id equals s.fk_used_for_encoding
                           join a in addedTable on p.pk_partiture_id equals a.fk_partiture_id
                           join u in usersTable on a.fk_user_id equals u.pk_user_id
                           where p.version_of == originalId
                           orderby p.pk_partiture_id
                           select p;
                           */

            System.Diagnostics.Debug.Print(versions.Count().ToString());
            foreach (var v in versions)
            { 
                userInfo = v.addedby.Single().users.full_name.TrimEnd() + "  " + v.addedby.Single().users.user_login.TrimEnd();
                softwareInfo = v.software.Single().software_name.TrimEnd() + "  " + v.software.Single().encoding_date + "  ";
                creatorsInfo = v.creators.SingleOrDefault().composer.TrimEnd() + "  " + v.creators.SingleOrDefault().poet.TrimEnd();
                partitureInfo = v.work_number.ToString().TrimEnd() + " " + v.title.TrimEnd() + " " + v.tempo.TrimEnd() + "  " + v.meter.TrimEnd() + "  " + v.note_length.TrimEnd() + "  " + v.part_key.TrimEnd() + "  ";
                info = partitureInfo + "  " + creatorsInfo + "  " + v.instruments.Single().instrument_name.TrimEnd() + "  " + softwareInfo + "  " + userInfo + "  " + v.filepath.TrimEnd();
                versionInfo.Add(info);
            }
            

            return versionInfo;
        }

        public List<string> GetVersionsFilePaths(int originalId)
        {
            List<string> result = (from p in partituresTable
                                   where p.version_of == originalId
                                   select p.filepath).ToList<string>();
            return result;
        }

        public IQueryable<users> GetUserByLogin(String login)
        {
            IQueryable<users> user = from u in usersTable
                                         where u.user_login == login
                                         select u;

            return user;
        }

        public IQueryable<addedby> GetOwner(int originalId)
        {
            IQueryable<addedby> owner = from a in addedTable
                                           join u in usersTable on a.fk_user_id equals u.pk_user_id
                                           join p in partituresTable on a.fk_partiture_id equals p.pk_partiture_id
                                           where p.pk_partiture_id == originalId
                                           select a;
            return owner;
        }
        public void updateOwner(int originalId, String login)
        {
            IQueryable<addedby> oldOwner = GetOwner(originalId);

            IQueryable<users> newOwner = GetUserByLogin(login);

            oldOwner.SingleOrDefault<addedby>().fk_user_id = newOwner.SingleOrDefault<users>().pk_user_id;
            /*
            foreach (var owner in oldOwner)
            {
                owner.fk_user_id = newOwner.SingleOrDefault<users>().pk_user_id;
            }
            */
            db.SubmitChanges();

        }

        public List<string> GetStatistics(int originalId)
        {
            int numberChanged = 0;
            int titleChanged = 0;
            int tempoChanged = 0;
            int meterChanged = 0;
            int keyChanged = 0;
            int notelengthChanged = 0;
            int composerChanged = 0;
            int poetChanged = 0;
            int softChanged = 0;

            string currentNumber = string.Empty;
            string currentTitle = string.Empty;
            string currentTempo = string.Empty;
            string currentMeter = string.Empty;
            string currentKey = string.Empty;
            string currentNoteLength = string.Empty;
            string currentComposer = string.Empty;
            string currentPoet = string.Empty;
            string currentSoft = string.Empty;

            List<string> statistics = new List<string>();

            var versions = getVersions(originalId);

            currentNumber = versions.First().work_number.TrimEnd();
            currentTitle = versions.First().title.TrimEnd();
            currentTempo = versions.First().tempo.TrimEnd();
            currentMeter = versions.First().meter.TrimEnd();
            currentKey = versions.First().part_key.TrimEnd();
            currentNoteLength = versions.First().note_length.TrimEnd();
            currentComposer = versions.First().creators.Single().composer.TrimEnd();
            currentPoet = versions.First().creators.Single().poet.TrimEnd();
            currentSoft = versions.First().software.Single().software_name.TrimEnd();

            foreach(var v in versions)
            {
                if (currentNumber != v.work_number.TrimEnd())
                {
                    numberChanged++;
                    currentNumber = v.work_number.TrimEnd();
                }
                if (currentTitle != v.title.TrimEnd())
                {
                    titleChanged++;
                    currentTitle = v.title.TrimEnd();
                }
                if(currentTempo != v.tempo.TrimEnd())
                {
                    tempoChanged++;
                    currentTempo = v.tempo.TrimEnd();
                }
                if(currentMeter != v.meter.TrimEnd())
                {
                    meterChanged++;
                    currentMeter = v.meter.TrimEnd();
                }
                if(currentKey != v.part_key.TrimEnd())
                {
                    keyChanged++;
                    currentKey = v.part_key.TrimEnd();
                }
                if(currentNoteLength != v.note_length.TrimEnd())
                {
                    notelengthChanged++;
                    currentNoteLength = v.note_length.TrimEnd();
                }
                if(currentComposer != v.creators.Single().composer.TrimEnd())
                {
                    composerChanged++;
                    currentComposer = v.creators.Single().composer.TrimEnd();
                }
                if(currentPoet != v.creators.Single().poet.TrimEnd())
                {
                    poetChanged++;
                    currentPoet = v.creators.Single().poet.TrimEnd();
                }
                if(currentSoft != v.software.Single().software_name.TrimEnd())
                {
                    softChanged++;
                    currentSoft = v.software.Single().software_name.TrimEnd();
                }
            }

            statistics.Add("Номер партитуры сменен " + numberChanged.ToString() + " раз");
            statistics.Add("Название сменено " + titleChanged.ToString() + " раз");
            statistics.Add("Темп сменен " + tempoChanged.ToString() + " раз");
            statistics.Add("Такт сменен " + meterChanged.ToString() + " раз");
            statistics.Add("Тональность сменена " + keyChanged.ToString() + " раз");
            statistics.Add("Длина ноты сменена " + notelengthChanged.ToString() + " раз");
            statistics.Add("Композитор сменен " + composerChanged.ToString() + " раз");
            statistics.Add("Поэт сменен " + poetChanged.ToString() + " раз");
            statistics.Add("Софт сменен " + softChanged.ToString() + " раз");

            return statistics;
        }

        public List<int> GetStatisticsNumber(int originalId)
        {
            int numberChanged = 0;
            int titleChanged = 0;
            int tempoChanged = 0;
            int meterChanged = 0;
            int keyChanged = 0;
            int notelengthChanged = 0;
            int composerChanged = 0;
            int poetChanged = 0;
            int softChanged = 0;

            string currentNumber = string.Empty;
            string currentTitle = string.Empty;
            string currentTempo = string.Empty;
            string currentMeter = string.Empty;
            string currentKey = string.Empty;
            string currentNoteLength = string.Empty;
            string currentComposer = string.Empty;
            string currentPoet = string.Empty;
            string currentSoft = string.Empty;

            List<int> statistics = new List<int>();

            var versions = getVersions(originalId);

            currentNumber = versions.First().work_number.TrimEnd();
            currentTitle = versions.First().title.TrimEnd();
            currentTempo = versions.First().tempo.TrimEnd();
            currentMeter = versions.First().meter.TrimEnd();
            currentKey = versions.First().part_key.TrimEnd();
            currentNoteLength = versions.First().note_length.TrimEnd();
            currentComposer = versions.First().creators.Single().composer.TrimEnd();
            currentPoet = versions.First().creators.Single().poet.TrimEnd();
            currentSoft = versions.First().software.Single().software_name.TrimEnd();

            foreach (var v in versions)
            {
                if (currentNumber != v.work_number.TrimEnd())
                {
                    numberChanged++;
                    currentNumber = v.work_number.TrimEnd();
                }
                if (currentTitle != v.title.TrimEnd())
                {
                    titleChanged++;
                    currentTitle = v.title.TrimEnd();
                }
                if (currentTempo != v.tempo.TrimEnd())
                {
                    tempoChanged++;
                    currentTempo = v.tempo.TrimEnd();
                }
                if (currentMeter != v.meter.TrimEnd())
                {
                    meterChanged++;
                    currentMeter = v.meter.TrimEnd();
                }
                if (currentKey != v.part_key.TrimEnd())
                {
                    keyChanged++;
                    currentKey = v.part_key.TrimEnd();
                }
                if (currentNoteLength != v.note_length.TrimEnd())
                {
                    notelengthChanged++;
                    currentNoteLength = v.note_length.TrimEnd();
                }
                if (currentComposer != v.creators.Single().composer.TrimEnd())
                {
                    composerChanged++;
                    currentComposer = v.creators.Single().composer.TrimEnd();
                }
                if (currentPoet != v.creators.Single().poet.TrimEnd())
                {
                    poetChanged++;
                    currentPoet = v.creators.Single().poet.TrimEnd();
                }
                if (currentSoft != v.software.Single().software_name.TrimEnd())
                {
                    softChanged++;
                    currentSoft = v.software.Single().software_name.TrimEnd();
                }
            }

                statistics.Add(numberChanged);
                statistics.Add(titleChanged);
                statistics.Add(tempoChanged);
                statistics.Add(meterChanged);
                statistics.Add(keyChanged);
                statistics.Add(notelengthChanged);
                statistics.Add(composerChanged);
                statistics.Add(poetChanged);
                statistics.Add(softChanged);
            System.Diagnostics.Debug.Print(statistics.Capacity.ToString());
                return statistics;
        }

        public List<string> GetStatisticsWholeBase()
        {

            List<int> statistics = new List<int>(9);

            for(int i = 0; i < statistics.Capacity; i++)
            {
                statistics.Add(0);
            }
            System.Diagnostics.Debug.Print(statistics[0].ToString());

            List<int> currentStatistics = null;
            List<string> information = new List<string>(9);
            for(int i = 0; i < information.Capacity; i++)
            {
                information.Add("");
            }

            var versionGroups = from p in partituresTable
                                group p by p.version_of
                      into g
                                select new
                                {
                                    original = g.Key,
                                    versions = from p in g
                                               orderby p.pk_partiture_id
                                               select p
                                };
            if (versionGroups == null)
                return information;

            foreach (var group in versionGroups)
            {
                currentStatistics = GetStatisticsNumber(group.versions.Last().version_of);
                for(int i = 0; i < statistics.Capacity; i++)
                {
                    statistics[i] += currentStatistics[i];
                }
            }

            information[0] = "Номер партитуры сменен " + statistics[0].ToString() + " раз";
            information[1] = "Название сменено " + statistics[1].ToString() + " раз";
            information[2] = "Темп сменен " + statistics[2].ToString() + " раз";
            information[3] = "Такт сменен " + statistics[3].ToString() + " раз";
            information[4] = "Тональность сменена " + statistics[4].ToString() + " раз";
            information[5] = "Длина ноты сменена " + statistics[5].ToString() + " раз";
            information[6] = "Композитор сменен " + statistics[6].ToString() + " раз";
            information[7] = "Поэт сменен " + statistics[7].ToString() + " раз";
            information[8] = "Софт сменен " + statistics[8].ToString() + " раз";

            return information;
        }
    }
}
