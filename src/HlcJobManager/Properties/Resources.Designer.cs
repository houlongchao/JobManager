﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HlcJobManager.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HlcJobManager.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 锁定 的本地化字符串。
        /// </summary>
        internal static string Blocked {
            get {
                return ResourceManager.GetString("Blocked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 确定要删除任务【{0}】吗？ 的本地化字符串。
        /// </summary>
        internal static string Comfirm_DeleteJob__jobname {
            get {
                return ResourceManager.GetString("Comfirm_DeleteJob__jobname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 完成 的本地化字符串。
        /// </summary>
        internal static string Complete {
            get {
                return ResourceManager.GetString("Complete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 确定要停止宿主服务（将会关闭所有任务）？ 的本地化字符串。
        /// </summary>
        internal static string Confirm_StopHost {
            get {
                return ResourceManager.GetString("Confirm_StopHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 确定要卸载宿主服务（将会关闭所有任务）？ 的本地化字符串。
        /// </summary>
        internal static string Confirm_UninstallHost {
            get {
                return ResourceManager.GetString("Confirm_UninstallHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 (-)一个减号，表示一次性服务，只启动一次。
        ///(0/10 * * * * ?)表示调度计划，每10秒运行一次
        ///调度计划由7段构成(秒 分 时 日 月 星期 年(可选))
        ///详细调度计划编写可查阅Quartz的cron表达式&quot; 的本地化字符串。
        /// </summary>
        internal static string CronHelp {
            get {
                return ResourceManager.GetString("CronHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 删除 的本地化字符串。
        /// </summary>
        internal static string Delete {
            get {
                return ResourceManager.GetString("Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 禁用 的本地化字符串。
        /// </summary>
        internal static string Disable {
            get {
                return ResourceManager.GetString("Disable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 启用 的本地化字符串。
        /// </summary>
        internal static string Enable {
            get {
                return ResourceManager.GetString("Enable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 出错 的本地化字符串。
        /// </summary>
        internal static string Error {
            get {
                return ResourceManager.GetString("Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找 System.Drawing.Bitmap 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Bitmap help_16px {
            get {
                object obj = ResourceManager.GetObject("help_16px", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   查找类似 调度计划格式有误，必须为6位或7位,或一次性服务任务&apos;-&apos; 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_CronErrorMessage {
            get {
                return ResourceManager.GetString("JobEditForm_CronErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 dll路径： 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_Lable_dllPath {
            get {
                return ResourceManager.GetString("JobEditForm_Lable_dllPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 exe路径： 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_Lable_exePath {
            get {
                return ResourceManager.GetString("JobEditForm_Lable_exePath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 工作目录： 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_Lable_workPath {
            get {
                return ResourceManager.GetString("JobEditForm_Lable_workPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 无cmd命令 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_NoCmd {
            get {
                return ResourceManager.GetString("JobEditForm_NoCmd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 选择工作目录 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_PleaseSelectWorkPath {
            get {
                return ResourceManager.GetString("JobEditForm_PleaseSelectWorkPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 工作文件不存在 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_WorkFileNotExist {
            get {
                return ResourceManager.GetString("JobEditForm_WorkFileNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 工作目录不存在 的本地化字符串。
        /// </summary>
        internal static string JobEditForm_WorkPathNotExist {
            get {
                return ResourceManager.GetString("JobEditForm_WorkPathNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 隐藏日志 的本地化字符串。
        /// </summary>
        internal static string LogView_Hide {
            get {
                return ResourceManager.GetString("LogView_Hide", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 查看日志 的本地化字符串。
        /// </summary>
        internal static string LogView_Show {
            get {
                return ResourceManager.GetString("LogView_Show", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 提示 的本地化字符串。
        /// </summary>
        internal static string MessageBox_Title_Information {
            get {
                return ResourceManager.GetString("MessageBox_Title_Information", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 警告 的本地化字符串。
        /// </summary>
        internal static string MessageBox_Title_Warning {
            get {
                return ResourceManager.GetString("MessageBox_Title_Warning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 无 的本地化字符串。
        /// </summary>
        internal static string None {
            get {
                return ResourceManager.GetString("None", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 正常 的本地化字符串。
        /// </summary>
        internal static string Normal {
            get {
                return ResourceManager.GetString("Normal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 未找到服务 的本地化字符串。
        /// </summary>
        internal static string NotFoundService {
            get {
                return ResourceManager.GetString("NotFoundService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 打开文件路径 的本地化字符串。
        /// </summary>
        internal static string OpenWorkFile {
            get {
                return ResourceManager.GetString("OpenWorkFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 打开工作目录 的本地化字符串。
        /// </summary>
        internal static string OpenWorkPath {
            get {
                return ResourceManager.GetString("OpenWorkPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 暂停 的本地化字符串。
        /// </summary>
        internal static string Paused {
            get {
                return ResourceManager.GetString("Paused", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 请先安装服务 的本地化字符串。
        /// </summary>
        internal static string PleaseInstallService {
            get {
                return ResourceManager.GetString("PleaseInstallService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 请先启动服务 的本地化字符串。
        /// </summary>
        internal static string PleaseStartService {
            get {
                return ResourceManager.GetString("PleaseStartService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务错误 的本地化字符串。
        /// </summary>
        internal static string ServiceError {
            get {
                return ResourceManager.GetString("ServiceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务已存在 的本地化字符串。
        /// </summary>
        internal static string ServiceExisted {
            get {
                return ResourceManager.GetString("ServiceExisted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务安装中 的本地化字符串。
        /// </summary>
        internal static string ServiceInstalling {
            get {
                return ResourceManager.GetString("ServiceInstalling", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 未发现服务 的本地化字符串。
        /// </summary>
        internal static string ServiceNotFound {
            get {
                return ResourceManager.GetString("ServiceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务正在运行 的本地化字符串。
        /// </summary>
        internal static string ServiceRunning {
            get {
                return ResourceManager.GetString("ServiceRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务启动中 的本地化字符串。
        /// </summary>
        internal static string ServiceStarting {
            get {
                return ResourceManager.GetString("ServiceStarting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务未运行 的本地化字符串。
        /// </summary>
        internal static string ServiceStoped {
            get {
                return ResourceManager.GetString("ServiceStoped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务停止中 的本地化字符串。
        /// </summary>
        internal static string ServiceStopping {
            get {
                return ResourceManager.GetString("ServiceStopping", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务被挂起 的本地化字符串。
        /// </summary>
        internal static string ServiceSuspend {
            get {
                return ResourceManager.GetString("ServiceSuspend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务暂停中 的本地化字符串。
        /// </summary>
        internal static string ServiceSuspending {
            get {
                return ResourceManager.GetString("ServiceSuspending", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务卸载中 的本地化字符串。
        /// </summary>
        internal static string ServiceUninstalling {
            get {
                return ResourceManager.GetString("ServiceUninstalling", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 未知状态 的本地化字符串。
        /// </summary>
        internal static string UnknownState {
            get {
                return ResourceManager.GetString("UnknownState", resourceCulture);
            }
        }
    }
}
