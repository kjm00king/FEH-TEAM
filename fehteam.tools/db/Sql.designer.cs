﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fehteam.tools.db
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="FehTeam")]
	public partial class SqlDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertHero(Hero instance);
    partial void UpdateHero(Hero instance);
    partial void DeleteHero(Hero instance);
    partial void InsertWeapon(Weapon instance);
    partial void UpdateWeapon(Weapon instance);
    partial void DeleteWeapon(Weapon instance);
    partial void InsertHeroEntity(HeroEntity instance);
    partial void UpdateHeroEntity(HeroEntity instance);
    partial void DeleteHeroEntity(HeroEntity instance);
    partial void InsertKVType(KVType instance);
    partial void UpdateKVType(KVType instance);
    partial void DeleteKVType(KVType instance);
    partial void InsertHeroDefWeapon(HeroDefWeapon instance);
    partial void UpdateHeroDefWeapon(HeroDefWeapon instance);
    partial void DeleteHeroDefWeapon(HeroDefWeapon instance);
    partial void InsertKV(KV instance);
    partial void UpdateKV(KV instance);
    partial void DeleteKV(KV instance);
    #endregion
		
		public SqlDataContext() : 
				base(global::fehteam.tools.Properties.Settings.Default.FehTeamConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Hero> Heros
		{
			get
			{
				return this.GetTable<Hero>();
			}
		}
		
		public System.Data.Linq.Table<Weapon> Weapons
		{
			get
			{
				return this.GetTable<Weapon>();
			}
		}
		
		public System.Data.Linq.Table<HeroEntity> HeroEntities
		{
			get
			{
				return this.GetTable<HeroEntity>();
			}
		}
		
		public System.Data.Linq.Table<KVType> KVTypes
		{
			get
			{
				return this.GetTable<KVType>();
			}
		}
		
		public System.Data.Linq.Table<HeroDefWeapon> HeroDefWeapons
		{
			get
			{
				return this.GetTable<HeroDefWeapon>();
			}
		}
		
		public System.Data.Linq.Table<KV> KVs
		{
			get
			{
				return this.GetTable<KV>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Hero")]
	public partial class Hero : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private int _WeaponClass;
		
		private int _Move;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnWeaponClassChanging(int value);
    partial void OnWeaponClassChanged();
    partial void OnMoveChanging(int value);
    partial void OnMoveChanged();
    #endregion
		
		public Hero()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(200) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WeaponClass", DbType="Int NOT NULL")]
		public int WeaponClass
		{
			get
			{
				return this._WeaponClass;
			}
			set
			{
				if ((this._WeaponClass != value))
				{
					this.OnWeaponClassChanging(value);
					this.SendPropertyChanging();
					this._WeaponClass = value;
					this.SendPropertyChanged("WeaponClass");
					this.OnWeaponClassChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Move", DbType="Int NOT NULL")]
		public int Move
		{
			get
			{
				return this._Move;
			}
			set
			{
				if ((this._Move != value))
				{
					this.OnMoveChanging(value);
					this.SendPropertyChanging();
					this._Move = value;
					this.SendPropertyChanged("Move");
					this.OnMoveChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Weapon")]
	public partial class Weapon : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private int _Damage;
		
		private int _Effect;
		
		private int _Range;
		
		private int _Cost;
		
		private bool _IsExclusive;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDamageChanging(int value);
    partial void OnDamageChanged();
    partial void OnEffectChanging(int value);
    partial void OnEffectChanged();
    partial void OnRangeChanging(int value);
    partial void OnRangeChanged();
    partial void OnCostChanging(int value);
    partial void OnCostChanged();
    partial void OnIsExclusiveChanging(bool value);
    partial void OnIsExclusiveChanged();
    #endregion
		
		public Weapon()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(200) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Damage", DbType="Int NOT NULL")]
		public int Damage
		{
			get
			{
				return this._Damage;
			}
			set
			{
				if ((this._Damage != value))
				{
					this.OnDamageChanging(value);
					this.SendPropertyChanging();
					this._Damage = value;
					this.SendPropertyChanged("Damage");
					this.OnDamageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Effect", DbType="Int NOT NULL")]
		public int Effect
		{
			get
			{
				return this._Effect;
			}
			set
			{
				if ((this._Effect != value))
				{
					this.OnEffectChanging(value);
					this.SendPropertyChanging();
					this._Effect = value;
					this.SendPropertyChanged("Effect");
					this.OnEffectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Range", DbType="Int NOT NULL")]
		public int Range
		{
			get
			{
				return this._Range;
			}
			set
			{
				if ((this._Range != value))
				{
					this.OnRangeChanging(value);
					this.SendPropertyChanging();
					this._Range = value;
					this.SendPropertyChanged("Range");
					this.OnRangeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Cost", DbType="Int NOT NULL")]
		public int Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				if ((this._Cost != value))
				{
					this.OnCostChanging(value);
					this.SendPropertyChanging();
					this._Cost = value;
					this.SendPropertyChanged("Cost");
					this.OnCostChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsExclusive", DbType="Bit NOT NULL")]
		public bool IsExclusive
		{
			get
			{
				return this._IsExclusive;
			}
			set
			{
				if ((this._IsExclusive != value))
				{
					this.OnIsExclusiveChanging(value);
					this.SendPropertyChanging();
					this._IsExclusive = value;
					this.SendPropertyChanged("IsExclusive");
					this.OnIsExclusiveChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.HeroEntity")]
	public partial class HeroEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _Hero;
		
		private int _Rarity;
		
		private int _Variation;
		
		private int _Level;
		
		private int _HP;
		
		private int _ATK;
		
		private int _SPD;
		
		private int _DEF;
		
		private int _RES;
		
		private int _W_ATK;
		
		private int _W_SPD;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnHeroChanging(int value);
    partial void OnHeroChanged();
    partial void OnRarityChanging(int value);
    partial void OnRarityChanged();
    partial void OnVariationChanging(int value);
    partial void OnVariationChanged();
    partial void OnLevelChanging(int value);
    partial void OnLevelChanged();
    partial void OnHPChanging(int value);
    partial void OnHPChanged();
    partial void OnATKChanging(int value);
    partial void OnATKChanged();
    partial void OnSPDChanging(int value);
    partial void OnSPDChanged();
    partial void OnDEFChanging(int value);
    partial void OnDEFChanged();
    partial void OnRESChanging(int value);
    partial void OnRESChanged();
    partial void OnW_ATKChanging(int value);
    partial void OnW_ATKChanged();
    partial void OnW_SPDChanging(int value);
    partial void OnW_SPDChanged();
    #endregion
		
		public HeroEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Hero", DbType="Int NOT NULL")]
		public int Hero
		{
			get
			{
				return this._Hero;
			}
			set
			{
				if ((this._Hero != value))
				{
					this.OnHeroChanging(value);
					this.SendPropertyChanging();
					this._Hero = value;
					this.SendPropertyChanged("Hero");
					this.OnHeroChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Rarity", DbType="Int NOT NULL")]
		public int Rarity
		{
			get
			{
				return this._Rarity;
			}
			set
			{
				if ((this._Rarity != value))
				{
					this.OnRarityChanging(value);
					this.SendPropertyChanging();
					this._Rarity = value;
					this.SendPropertyChanged("Rarity");
					this.OnRarityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Variation", DbType="Int NOT NULL")]
		public int Variation
		{
			get
			{
				return this._Variation;
			}
			set
			{
				if ((this._Variation != value))
				{
					this.OnVariationChanging(value);
					this.SendPropertyChanging();
					this._Variation = value;
					this.SendPropertyChanged("Variation");
					this.OnVariationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Level]", Storage="_Level", DbType="Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if ((this._Level != value))
				{
					this.OnLevelChanging(value);
					this.SendPropertyChanging();
					this._Level = value;
					this.SendPropertyChanged("Level");
					this.OnLevelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HP", DbType="Int NOT NULL")]
		public int HP
		{
			get
			{
				return this._HP;
			}
			set
			{
				if ((this._HP != value))
				{
					this.OnHPChanging(value);
					this.SendPropertyChanging();
					this._HP = value;
					this.SendPropertyChanged("HP");
					this.OnHPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ATK", DbType="Int NOT NULL")]
		public int ATK
		{
			get
			{
				return this._ATK;
			}
			set
			{
				if ((this._ATK != value))
				{
					this.OnATKChanging(value);
					this.SendPropertyChanging();
					this._ATK = value;
					this.SendPropertyChanged("ATK");
					this.OnATKChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SPD", DbType="Int NOT NULL")]
		public int SPD
		{
			get
			{
				return this._SPD;
			}
			set
			{
				if ((this._SPD != value))
				{
					this.OnSPDChanging(value);
					this.SendPropertyChanging();
					this._SPD = value;
					this.SendPropertyChanged("SPD");
					this.OnSPDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DEF", DbType="Int NOT NULL")]
		public int DEF
		{
			get
			{
				return this._DEF;
			}
			set
			{
				if ((this._DEF != value))
				{
					this.OnDEFChanging(value);
					this.SendPropertyChanging();
					this._DEF = value;
					this.SendPropertyChanged("DEF");
					this.OnDEFChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RES", DbType="Int NOT NULL")]
		public int RES
		{
			get
			{
				return this._RES;
			}
			set
			{
				if ((this._RES != value))
				{
					this.OnRESChanging(value);
					this.SendPropertyChanging();
					this._RES = value;
					this.SendPropertyChanged("RES");
					this.OnRESChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_W_ATK", DbType="Int NOT NULL")]
		public int W_ATK
		{
			get
			{
				return this._W_ATK;
			}
			set
			{
				if ((this._W_ATK != value))
				{
					this.OnW_ATKChanging(value);
					this.SendPropertyChanging();
					this._W_ATK = value;
					this.SendPropertyChanged("W_ATK");
					this.OnW_ATKChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_W_SPD", DbType="Int NOT NULL")]
		public int W_SPD
		{
			get
			{
				return this._W_SPD;
			}
			set
			{
				if ((this._W_SPD != value))
				{
					this.OnW_SPDChanging(value);
					this.SendPropertyChanging();
					this._W_SPD = value;
					this.SendPropertyChanged("W_SPD");
					this.OnW_SPDChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.KVType")]
	public partial class KVType : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public KVType()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.HeroDefWeapon")]
	public partial class HeroDefWeapon : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Hero;
		
		private int _Rarity;
		
		private int _Weapon;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnHeroChanging(int value);
    partial void OnHeroChanged();
    partial void OnRarityChanging(int value);
    partial void OnRarityChanged();
    partial void OnWeaponChanging(int value);
    partial void OnWeaponChanged();
    #endregion
		
		public HeroDefWeapon()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Hero", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Hero
		{
			get
			{
				return this._Hero;
			}
			set
			{
				if ((this._Hero != value))
				{
					this.OnHeroChanging(value);
					this.SendPropertyChanging();
					this._Hero = value;
					this.SendPropertyChanged("Hero");
					this.OnHeroChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Rarity", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Rarity
		{
			get
			{
				return this._Rarity;
			}
			set
			{
				if ((this._Rarity != value))
				{
					this.OnRarityChanging(value);
					this.SendPropertyChanging();
					this._Rarity = value;
					this.SendPropertyChanged("Rarity");
					this.OnRarityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weapon", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Weapon
		{
			get
			{
				return this._Weapon;
			}
			set
			{
				if ((this._Weapon != value))
				{
					this.OnWeaponChanging(value);
					this.SendPropertyChanging();
					this._Weapon = value;
					this.SendPropertyChanged("Weapon");
					this.OnWeaponChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.KV")]
	public partial class KV : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private int _Type;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnTypeChanging(int value);
    partial void OnTypeChanged();
    #endregion
		
		public KV()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
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
