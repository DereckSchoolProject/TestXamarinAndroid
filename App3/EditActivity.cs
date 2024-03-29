﻿using Android.App;
using Android.OS;
using Android.Widget;
using App3.Database;

namespace App3
{
    [Activity(Label = "EditActivity")]
    public class EditActivity : BaseActivity
    {
        #region Const
        public const string EXTRA_ID = "EXTRA_ID";
        public const string EXTRA_EDIT_CODE = "EXTRA_EDIT_CODE";
        #endregion

        #region Attributs
        private TextView tvLibel;
        private EditText etValue;
        private Button btnCancel;
        private Button btnValidate;

        private Entity editedItem;
        private int editCode;
        #endregion

        #region OverridedMethods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (this.Intent != null)
            {
                this.editedItem = SingletonEntity.Instance.Find(this.Intent.GetIntExtra(EXTRA_ID, 0));

                this.editCode = this.Intent.GetIntExtra(EXTRA_EDIT_CODE, 0);
                string libel = "";

                switch (this.editCode)
                {
                    case 0:
                        libel = $"Ess of {this.editedItem.Num}";
                        this.etValue.Text = this.editedItem.Ess;
                        break;
                    case 1:
                        libel = $"Diam1 of {this.editedItem.Num}";
                        this.etValue.Text = this.editedItem.Diam1.ToString();
                        break;
                    case 2:
                        libel = $"Diam2 of {this.editedItem.Num}";
                        this.etValue.Text = this.editedItem.Diam2.ToString();
                        break;
                    default:
                        libel = $"{this.editedItem.Num}";
                        break;
                }
                this.tvLibel.Text = libel;
            }
        }

        protected override void InitComponents()
        {
            this.tvLibel = this.FindViewById<TextView>(Resource.Id.tv_libel);
            this.etValue = this.FindViewById<EditText>(Resource.Id.et_value);
            this.btnCancel = this.FindViewById<Button>(Resource.Id.btn_cancel);
            this.btnValidate = this.FindViewById<Button>(Resource.Id.btn_validate);
        }


        protected override void InitEvents()
        {
            this.btnCancel.Click += (sender, e) =>
            {
                this.Finish();
            };

            this.btnValidate.Click += (sender, e) =>
            {
                int val = 0;

                switch (this.editCode)
                {
                    case 0:
                        this.editedItem.Ess = this.etValue.Text;
                        break;
                    case 1:
                        if (int.TryParse(this.etValue.Text, out val))
                        {
                            this.editedItem.Diam1 = val;
                        }
                        break;
                    case 2:
                        if (int.TryParse(this.etValue.Text, out val))
                        {
                            this.editedItem.Diam2 = val;
                        }
                        break;
                    default:
                        if (int.TryParse(this.etValue.Text, out val))
                        {
                            this.editedItem.Num = val;
                        }
                        break;
                }

                SingletonEntity.Instance.InsertOrUpdate(this.editedItem);
                this.Finish();
            };
        }

        public override int GetContentView()
        {
            return Resource.Layout.activity_edit;
        }
        #endregion
    }
}
