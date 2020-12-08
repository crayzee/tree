using Omu.AwesomeMvc;
using System;
using System.Collections.Generic;

namespace Omu.Awem.Helpers
{
    /// <summary>
    /// ASP.net MVC Awesome mod extensions
    /// </summary>
    public static class AwesomeModExtensions
    {
        private static void setnv(ref string s, string s1)
        {
            if (s1 != null) s = s1;
        }

        private static string GetChkedClass(object cfgVal)
        {
            var value = false;

            if (cfgVal != null)
            {
                var type = cfgVal.GetType();
                if (type == typeof(bool))
                {
                    value = (bool)cfgVal;
                }
                else if (type == typeof(string))
                {
                    if ((string)cfgVal == "true")
                    {
                        value = true;
                    }
                }
            }

            return value ? "o-chked" : string.Empty;
        }

        /// <summary>
        /// Ocheckbox
        /// </summary>
        /// <param name="chk"></param>
        /// <param name="setCfg"></param>
        /// <param name="enabled"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static CheckBox Otoggl(this CheckBox chk, Action<OtogglCfg> setCfg = null, bool enabled = true, string cssClass = null)
        {
            if (enabled)
            {
                string yes = "Yes", no = "No";
                var style = "";

                var oc = new OtogglCfg();

                if (setCfg != null)
                {
                    setCfg(oc);
                }

                var tag = oc.GetTag();

                if (Settings.GetText != null)
                {
                    setnv(ref yes, Settings.GetText("Otoggl", "Yes"));
                    setnv(ref no, Settings.GetText("Otoggl", "No"));
                }

                setnv(ref yes, tag.Yes);
                setnv(ref no, tag.No);

                if (tag.Width != null)
                {
                    style = " style=\"width:" + tag.Width + ";\"";
                }

                Func<CheckboxCfg, string> renderFunc = cfg =>
                {
                    var tabAttr = "tabindex=\"0\"";
                    var chkedcls = GetChkedClass(cfg.Value);

                    if (!cfg.Enabled)
                    {
                        tabAttr = "";
                    }

                    cssClass = cssClass ?? string.Empty;

                    var schk = "<input " + AweHelperUtil.DictToHtmlAttr(cfg.EditorAttrs) + " style='display:none;' />";

                    var str =
                        "<div " + tabAttr + style + " class=\"o-tg " + chkedcls + " " + cssClass + "\">" +
                            "<div class=\"o-tgg\">" +
                                "<div class=\"o-tgon\"><span class=\"o-ccon\">" + yes + "</span></div>" +
                                "<div class=\"o-tgoff\"><span class=\"o-ccon\">" + no + "</span></div>" +
                                "<div class=\"o-tgh awe-btn o-btn\"></div>" +
                            "</div>" +
                        "</div>" +
                        schk;
                    return str;
                };

                chk.RenderDisp(renderFunc);
            }
            else
            {
                chk.RenderDisp(null);
            }

            chk.Mod(enabled ? "awem.otggl" : "awem.empf");
            return chk;
        }

        /// <summary>
        /// Ocheckbox
        /// </summary>
        /// <param name="chk"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public static CheckBox Ochk(this CheckBox chk, bool enabled = true)
        {
            if (enabled)
            {
                Func<CheckboxCfg, string> renderFunc = cfg =>
                {
                    var tabAttr = "tabindex=\"0\"";
                    var chkedcls = GetChkedClass(cfg.Value);

                    if (!cfg.Enabled)
                    {
                        tabAttr = "";
                    }

                    var schk = "<input " + AweHelperUtil.DictToHtmlAttr(cfg.EditorAttrs) + " style='display:none;' />";
                    var str = "<div class=\"o-chk " + chkedcls + "\"><div class=\"o-chkc\"><div " + tabAttr + " class=\"o-chkico\"></div></div></div>" + schk;
                    return str;
                };

                chk.RenderDisp(renderFunc);
            }
            else
            {
                chk.RenderDisp(null);
            }

            chk.Mod(enabled ? "awem.ochk" : "awem.empf");
            return chk;
        }

        /// <summary>
        /// grid inside the lookup popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lookup"></param>
        /// <param name="popupUrl"></param>
        /// <returns></returns>
        public static Lookup<T> LookupGrid<T>(this Lookup<T> lookup, string popupUrl)
        {
            lookup.PopupUrl(popupUrl);
            lookup.Mod("awem.lookupGrid");
            return lookup;
        }

        /// <summary>
        /// grid inside the multilookup popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="multilookup"></param>
        /// <param name="popupUrl"></param>
        /// <returns></returns>
        public static MultiLookup<T> MultiLookupGrid<T>(this MultiLookup<T> multilookup, string popupUrl)
        {
            multilookup.PopupUrl(popupUrl);
            multilookup.Mod("awem.multilookupGrid");
            return multilookup;
        }

        /// <summary>
        /// Ochk extension
        /// </summary>
        public static AjaxCheckboxList<T> Ochk<T>(this AjaxCheckboxList<T> arl, Action<OchklCfg> setCfg = null)
        {
            var res = arl.Mod("awem.ochkl");

            if (setCfg != null)
            {
                var odcfg = new OchklCfg();
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        /// <summary>
        /// Ochk extension
        /// </summary>
        public static AjaxRadioList<T> Ochk<T>(this AjaxRadioList<T> arl, Action<OchklCfg> setCfg = null)
        {
            var res = arl.Mod("awem.ochkl");

            if (setCfg != null)
            {
                var odcfg = new OchklCfg();
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        /// <summary>
        /// Odropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> Odropdown<T>(this AjaxRadioList<T> arl, Action<OddCfg> setCfg = null)
        {
            var res = arl.Mod("awem.odropdown");

            if (setCfg != null)
            {
                var odcfg = new OddCfg();
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        /// <summary>
        /// menu dropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> MenuDropdown<T>(this AjaxRadioList<T> arl, Action<OddCfg> setCfg = null)
        {
            var res = arl.Mod("awem.menuDropdown");
            var odcfg = new OddCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        /// <summary>
        /// color dropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> ColorDropdown<T>(this AjaxRadioList<T> arl, Action<OddCfg> setCfg = null)
        {
            arl.Mod("awem.colorDropdown");
            arl.AutoUrl(false);
            var odcfg = new OddCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// combobox extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> Combobox<T>(this AjaxRadioList<T> arl, Action<ComboboxCfg> setCfg = null)
        {
            arl.Mod("awem.combobox");
            var odcfg = new ComboboxCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// timepicker extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> TimePicker<T>(this AjaxRadioList<T> arl, Action<TimePickerCfg> setCfg = null)
        {
            arl.Mod("awem.timepicker");
            arl.UnobsValid(false);
            arl.AutoUrl(false);

            var cfg = new TimePickerCfg();
            var tag = new TimePickerTag();

            if (setCfg != null)
            {
                setCfg(cfg);
                tag = cfg.ToTag();
            }

            var cformat = Autil.CurrentCulture().DateTimeFormat;
            var isAmPm = cformat.ShortTimePattern.Contains("h");

            if (isAmPm)
            {
                tag.AmPm = new[] { cformat.AMDesignator, cformat.PMDesignator };
            }

            arl.Tag(tag);

            arl.ValueRenderer(
                o =>
                {
                    if (o != null)
                    {
                        if (o is DateTime)
                        {
                            return ((DateTime)o).ToString(cformat.ShortTimePattern);
                        }

                        return o.ToString();
                    }

                    return string.Empty;
                });
            return arl;
        }

        /// <summary>
        /// imgdropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> ImgDropdown<T>(this AjaxRadioList<T> arl, Action<OddCfg> setCfg = null)
        {
            arl.Mod("awem.imgDropdown");
            var odcfg = new OddCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// buttongroup extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> ButtonGroup<T>(this AjaxRadioList<T> arl, Action<OddCfg> setCfg = null)
        {
            arl.Mod("awem.btnGroup");
            var odcfg = new OddCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// buttongroup extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxCheckboxList<T> ButtonGroup<T>(this AjaxCheckboxList<T> arl, Action<OddCfg> setCfg = null)
        {
            arl.Mod("awem.btnGroupChk");
            var odcfg = new OddCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// multiselect extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxCheckboxList<T> Multiselect<T>(this AjaxCheckboxList<T> arl, Action<MultiselectCfg> setCfg = null)
        {
            arl.Mod("awem.multiselect");
            var odcfg = new MultiselectCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
            }

            arl.Tag(odcfg.ToTag());

            return arl;
        }

        /// <summary>
        /// multichk extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxCheckboxList<T> Multichk<T>(this AjaxCheckboxList<T> arl, Action<OddCfg> setCfg = null)
        {
            arl.Mod("awem.multichk");
            var odcfg = new OddCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
            }

            arl.Tag(odcfg.ToTag());

            return arl;
        }

        /// <summary>
        /// dropdown popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static InitPopup<T> Mod<T>(this InitPopup<T> o, Action<PopupCfg> setCfg = null)
        {
            var cfg = new PopupCfg();
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());
            return o;
        }

        /// <summary>
        /// dropdown popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static InitPopupForm<T> Mod<T>(this InitPopupForm<T> o, Action<PopupCfg> setCfg = null)
        {
            var cfg = new PopupCfg();
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());
            return o;
        }

        /// <summary>
        /// Lookup mods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static Lookup<T> Mod<T>(this Lookup<T> o, Action<LookupModCfg> setCfg = null)
        {
            var cfg = new LookupModCfg(o.Awe);
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());

            return o;
        }

        /// <summary>
        /// Lookup mods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static MultiLookup<T> Mod<T>(this MultiLookup<T> o, Action<LookupModCfg> setCfg = null)
        {
            var cfg = new LookupModCfg(o.Awe);
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());

            return o;
        }

        /// <summary>
        /// grid mods extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static Grid<T> Mod<T>(this Grid<T> grid, Action<GridModCfg<T>> setCfg = null)
        {
            if (setCfg == null) return grid;

            var cfg = new GridModCfg<T>(grid.Awe, grid);
            setCfg(cfg);
            var gmi = cfg.GetInfo();
            var mods = new List<string>();

            if (gmi.PageSize) mods.Add("awem.gridPageSize");
            if (gmi.ColumnsSelector) mods.Add("awem.gridColSel");
            if (gmi.PageInfo) mods.Add("awem.gridPageInfo");
            if (gmi.InfiniteScroll) mods.Add("awem.gridInfScroll");
            if (gmi.KeyNav) mods.Add("awem.gridKeyNav");

            if (gmi.AutoMiniPager)
            {
                mods.Add("awem.gridAutoMiniPager");
            }
            else if (gmi.MiniPager)
            {
                mods.Add("awem.gridMiniPager");
            }

            if (gmi.FzLeft > 0 || gmi.FzRight > 0)
            {
                mods.Add("awem.gridFreezeColumns({ left: " + gmi.FzLeft + ", right: " + gmi.FzRight + ", minsw: " + gmi.Minsw + "})");
            }

            if (gmi.InlineEdit)
            {
                mods.Add("awem.gridInlineEdit('"
                         + gmi.CreateUrl + "','"
                         + gmi.EditUrl + "', "
                         + gmi.OneRow.ToString().ToLower() + ","
                         + gmi.RelOnSav.ToString().ToLower() + ","
                         + gmi.RowClickEdit.ToString().ToLower() + ")");
            }

            if (gmi.FilterOpt != null)
            {
                mods.Add(string.Format("awem.gridFilterRow({0})", Autil.Serialize(gmi.FilterOpt)));
            }

            if (gmi.Loading)
            {
                mods.Add(string.Format("awem.gldng({0}, {1})", gmi.LdngDisb ? 1 : 0, gmi.NoEmpMsg ? 1 : 0));
                grid.Props.Add("empMsg", gmi.EmptyMessage);
            }

            var movRowsOpt = new { G = gmi.GridIds };
            if (gmi.GridIds == null || gmi.GridIds.Length == 0)
            {
                movRowsOpt = null;
            }

            if (gmi.MovableRows)
            {
                mods.Add(string.Format("awem.gridMovRows({0})", Autil.Serialize(movRowsOpt)));
            }

            if (gmi.CustomRender)
            {
                var cropt = new { mdf = gmi.modFunc };
                mods.Add(string.Format("awem.gridCstRen({0})", Autil.Serialize(cropt)));
            }

            if (gmi.CustomMods != null)
            {
                mods.AddRange(gmi.CustomMods);
            }

            grid.Mod(mods.ToArray());

            grid.BeforeRenderFuncs.Add(g =>
            {
                var autohide = false;
                foreach (var column in g.GetColumns())
                {
                    var colModTag = column.Tag as ColumnModTag;

                    if (gmi.ColumnsAutohide)
                    {
                        autohide = true;

                        if (colModTag == null)
                        {
                            colModTag = new ColumnModTag();
                            column.Tag = colModTag;
                        }

                        if (!colModTag.Autohide.HasValue)
                        {
                            colModTag.Autohide = 1;
                        }

                        if (colModTag.Nohide)
                        {
                            colModTag.Autohide = 0;
                        }
                    }
                    else if (colModTag != null && colModTag.Autohide > 0)
                    {
                        autohide = true;
                    }
                }

                if (autohide)
                {
                    g.GetMods().Add("awem.gridColAutohide");
                }
            });

            return grid;
        }

        /// <summary>
        /// grid Column mod extension
        /// </summary>
        /// <param name="column"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static Column Mod(this Column column, Action<ColumnModCfg> setCfg = null)
        {
            if (setCfg != null)
            {
                var cfg = new ColumnModCfg(column);
                setCfg(cfg);
                var info = cfg.GetTag();
                column.Tag = info;
            }

            return column;
        }
    }
}