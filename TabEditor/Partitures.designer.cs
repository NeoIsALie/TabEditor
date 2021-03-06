﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BDCourse
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PARTITURES")]
	public partial class PartituresDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertAuthors(Authors instance);
    partial void UpdateAuthors(Authors instance);
    partial void DeleteAuthors(Authors instance);
    partial void InsertInstruments(Instruments instance);
    partial void UpdateInstruments(Instruments instance);
    partial void DeleteInstruments(Instruments instance);
    partial void InsertPartitures(Partitures instance);
    partial void UpdatePartitures(Partitures instance);
    partial void DeletePartitures(Partitures instance);
    #endregion
		
		public PartituresDataContext() : 
				base(global::BDCourse.Properties.Settings.Default.PARTITURESConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PartituresDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PartituresDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PartituresDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PartituresDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Authors> Authors
		{
			get
			{
				return this.GetTable<Authors>();
			}
		}
		
		public System.Data.Linq.Table<Connection> Connection
		{
			get
			{
				return this.GetTable<Connection>();
			}
		}
		
		public System.Data.Linq.Table<Instruments> Instruments
		{
			get
			{
				return this.GetTable<Instruments>();
			}
		}
		
		public System.Data.Linq.Table<Partitures> Partitures
		{
			get
			{
				return this.GetTable<Partitures>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Authors")]
	public partial class Authors : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Pk_Author_ID;
		
		private string _FullName;
		
		private System.Nullable<int> _Age;
		
		private string _Gender;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPk_Author_IDChanging(int value);
    partial void OnPk_Author_IDChanged();
    partial void OnFullNameChanging(string value);
    partial void OnFullNameChanged();
    partial void OnAgeChanging(System.Nullable<int> value);
    partial void OnAgeChanged();
    partial void OnGenderChanging(string value);
    partial void OnGenderChanged();
    #endregion
		
		public Authors()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pk_Author_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Pk_Author_ID
		{
			get
			{
				return this._Pk_Author_ID;
			}
			set
			{
				if ((this._Pk_Author_ID != value))
				{
					this.OnPk_Author_IDChanging(value);
					this.SendPropertyChanging();
					this._Pk_Author_ID = value;
					this.SendPropertyChanged("Pk_Author_ID");
					this.OnPk_Author_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullName", DbType="NChar(100)")]
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				if ((this._FullName != value))
				{
					this.OnFullNameChanging(value);
					this.SendPropertyChanging();
					this._FullName = value;
					this.SendPropertyChanged("FullName");
					this.OnFullNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Age", DbType="Int")]
		public System.Nullable<int> Age
		{
			get
			{
				return this._Age;
			}
			set
			{
				if ((this._Age != value))
				{
					this.OnAgeChanging(value);
					this.SendPropertyChanging();
					this._Age = value;
					this.SendPropertyChanged("Age");
					this.OnAgeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="NChar(6)")]
		public string Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this.OnGenderChanging(value);
					this.SendPropertyChanging();
					this._Gender = value;
					this.SendPropertyChanged("Gender");
					this.OnGenderChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Connection")]
	public partial class Connection
	{
		
		private int _Fk_Authors_ID;
		
		private int _Fk_Partitures_ID;
		
		private int _Fk_Instruments_ID;
		
		public Connection()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Fk_Authors_ID", DbType="Int NOT NULL")]
		public int Fk_Authors_ID
		{
			get
			{
				return this._Fk_Authors_ID;
			}
			set
			{
				if ((this._Fk_Authors_ID != value))
				{
					this._Fk_Authors_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Fk_Partitures_ID", DbType="Int NOT NULL")]
		public int Fk_Partitures_ID
		{
			get
			{
				return this._Fk_Partitures_ID;
			}
			set
			{
				if ((this._Fk_Partitures_ID != value))
				{
					this._Fk_Partitures_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Fk_Instruments_ID", DbType="Int NOT NULL")]
		public int Fk_Instruments_ID
		{
			get
			{
				return this._Fk_Instruments_ID;
			}
			set
			{
				if ((this._Fk_Instruments_ID != value))
				{
					this._Fk_Instruments_ID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Instruments")]
	public partial class Instruments : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Pk_Instrument_ID;
		
		private string _Instrument_Name;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPk_Instrument_IDChanging(int value);
    partial void OnPk_Instrument_IDChanged();
    partial void OnInstrument_NameChanging(string value);
    partial void OnInstrument_NameChanged();
    #endregion
		
		public Instruments()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pk_Instrument_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Pk_Instrument_ID
		{
			get
			{
				return this._Pk_Instrument_ID;
			}
			set
			{
				if ((this._Pk_Instrument_ID != value))
				{
					this.OnPk_Instrument_IDChanging(value);
					this.SendPropertyChanging();
					this._Pk_Instrument_ID = value;
					this.SendPropertyChanged("Pk_Instrument_ID");
					this.OnPk_Instrument_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Instrument_Name", DbType="NChar(50)")]
		public string Instrument_Name
		{
			get
			{
				return this._Instrument_Name;
			}
			set
			{
				if ((this._Instrument_Name != value))
				{
					this.OnInstrument_NameChanging(value);
					this.SendPropertyChanging();
					this._Instrument_Name = value;
					this.SendPropertyChanged("Instrument_Name");
					this.OnInstrument_NameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Partitures")]
	public partial class Partitures : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Pk_Partiture_ID;
		
		private string _Partiture_Name;
		
		private string _Partiture_Author;
		
		private string _Partiture_Path;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPk_Partiture_IDChanging(int value);
    partial void OnPk_Partiture_IDChanged();
    partial void OnPartiture_NameChanging(string value);
    partial void OnPartiture_NameChanged();
    partial void OnPartiture_AuthorChanging(string value);
    partial void OnPartiture_AuthorChanged();
    partial void OnPartiture_PathChanging(string value);
    partial void OnPartiture_PathChanged();
    #endregion
		
		public Partitures()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pk_Partiture_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Pk_Partiture_ID
		{
			get
			{
				return this._Pk_Partiture_ID;
			}
			set
			{
				if ((this._Pk_Partiture_ID != value))
				{
					this.OnPk_Partiture_IDChanging(value);
					this.SendPropertyChanging();
					this._Pk_Partiture_ID = value;
					this.SendPropertyChanged("Pk_Partiture_ID");
					this.OnPk_Partiture_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Partiture_Name", DbType="NChar(50)")]
		public string Partiture_Name
		{
			get
			{
				return this._Partiture_Name;
			}
			set
			{
				if ((this._Partiture_Name != value))
				{
					this.OnPartiture_NameChanging(value);
					this.SendPropertyChanging();
					this._Partiture_Name = value;
					this.SendPropertyChanged("Partiture_Name");
					this.OnPartiture_NameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Partiture_Author", DbType="NChar(50)")]
		public string Partiture_Author
		{
			get
			{
				return this._Partiture_Author;
			}
			set
			{
				if ((this._Partiture_Author != value))
				{
					this.OnPartiture_AuthorChanging(value);
					this.SendPropertyChanging();
					this._Partiture_Author = value;
					this.SendPropertyChanged("Partiture_Author");
					this.OnPartiture_AuthorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Partiture_Path", DbType="NChar(255)")]
		public string Partiture_Path
		{
			get
			{
				return this._Partiture_Path;
			}
			set
			{
				if ((this._Partiture_Path != value))
				{
					this.OnPartiture_PathChanging(value);
					this.SendPropertyChanging();
					this._Partiture_Path = value;
					this.SendPropertyChanged("Partiture_Path");
					this.OnPartiture_PathChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
