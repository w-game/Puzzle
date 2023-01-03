#if UNITY_ANDROID

using UnityEditor;
using UnityEditor.Android;
using UnityEngine;
using System.Linq;
using System.IO;

class AndroidPostBuildProcessor : IPostGenerateGradleAndroidProject
{
    private string privacyAppName = Application.productName;
    private string privacyEmail = "chenglong.nie@outlook.com";

    public int callbackOrder { get { return 999; } }
    public void OnPostGenerateGradleAndroidProject(string path)
    {
        Debug.Log("AndroidPostBuildProcessor.OnPostGenerateGradleAndroidProject at path " + path);

        string unityPlayerJavaFilePath = path + "/src/main/java/com/unity3d/player/UnityPlayerActivity.java";

        string content = File.ReadAllText(unityPlayerJavaFilePath);

        content = content.Replace("import android.os.Process;", "import android.os.Process;\n\n" + privacyImport);
        content = content.Replace("mUnityPlayer = new UnityPlayer(this, this);", privacyString.Replace("{APP_NAME}", privacyAppName).Replace("{APP_EMAIL}", privacyEmail) + "\n" + "mUnityPlayer = new UnityPlayer(this, this);");

        File.WriteAllText(unityPlayerJavaFilePath, content);
    }

    private string privacyImport = @"
import android.app.AlertDialog;
import android.content.SharedPreferences;
import android.content.DialogInterface;
";

    private string privacyString = @"
// 先展示隐私政策
        SharedPreferences base = getSharedPreferences(""base"", MODE_PRIVATE);
        Boolean privacyFlag = base.getBoolean(""PrivacyFlag"", true);
//      privacyFlag = true;
        if (privacyFlag == true) {
            AlertDialog.Builder dialog = new AlertDialog.Builder(UnityPlayerActivity.this);
            dialog.setTitle(""隐私政策"");  //设置标题
            dialog.setMessage("""" +
                    ""作为“{APP_NAME}”的运营者，深知个人信息对您的重要性，我们将按照法律法规的规定，保护您的个人信息及隐私安全。我们制定本“隐私政策”并特别提示：希望您在使用“{APP_NAME}”及相关服务前仔细阅读并理解本隐私政策，以便做出适当的选择。\n"" +
//                  ""应监管要求,请你务必审慎阅读、充分理解“隐私政策”各条款，包括但不限于：为了向你提供精准广告、优化游戏体验等，我们需要收集你的设备信息等个人信息。\n"" +
                    ""如你同意，请点击“同意”开始进入游戏。\n"" +
                    ""\n"" +
                    ""本隐私政策将帮助您了解：\n"" +
                    ""1、我们会遵循隐私政策收集、使用您的信息，但不会仅因您同意本隐私政策而采用强制捆绑的方式一揽子收集个人信息。\n"" +
                    ""2、当您使用或开启相关功能或使用服务时，为实现功能、服务所必需，我们会收集、使用相关信息。\n"" +
                    ""3、相关敏感权限均不会默认开启，只有经过您的明示授权才会在为实现特定功能或服务时使用，您也可以撤回授权。特别需要指出的是，即使经过您的授权，我们获得了这些敏感权限，也不会在相关功能或服务不需要时而收集您的信息。\n"" +
                    ""4、本隐私政策适用于您通过“{APP_NAME}”应用程序、供第三方网站和应用程序使用的软件开发工具包（SDK）和应用程序编程接口（API）方式来访问和使用我们的产品和服务。\n"" +
                    ""\n"" +
                    ""下文将帮您详细了解我们如何收集、使用、存储、传输与保护个人信息；帮您了解查询、访问、删除、更正、撤回授权个人信息的方式。\n"" +
                    ""1.我们如何收集和使用个人信息\n"" +
                    ""2.我们如何使用Cookie等同类技术\n"" +
                    ""3.我们如何共享、转让、公开披露个人信息\n"" +
                    ""4.我们如何存储个人信息\n"" +
                    ""5.我们如何保护个人信息的安全\n"" +
                    ""6.管理您的个人信息\n"" +
                    ""7.未成年人条款\n"" +
                    ""8.隐私政策的修订和通知\n"" +
                    ""9.联系我们\n"" +
                    ""\n"" +

                    ""1.我们如何收集和使用个人信息\n"" +
                    ""我们会按照如下方式收集您在使用服务时主动提供的，以及通过自动化手段收集您在使用功能或接受服务过程中产生的信息：\n"" +
                    ""1.1 满足实名认证要求\n"" +
                    ""为满足相关法律法规政策及相关主管部门的要求，您需要进行实名认证以继续使用和享受本软件及相关服务。我们会在获得您同意或您主动提供的情况下收集您的实名身份信息，该信息属于敏感信息，拒绝提供实名身份信息可能会导致您无法登陆本游戏及相关服务或在使用本软件及相关服务过程中受到相应限制；或我们会在您授权同意的情况下，获得您在本游戏的独立APP版本上的实名认证结果。\n"" +
                    ""1.2 运营与安全保障\n"" +
                    ""1.2.1 运营与安全\n"" +
                    ""我们致力于为您提供安全、可信的产品与使用环境，提供优质、高效、可靠的服务与信息是我们的核心目标。\n"" +
                    ""1.2.2 设备信息与日志信息\n"" +
                    ""a.为了保障软件服务的安全、运营的质量及效率，我们会收集您的硬件型号、操作系统版本号、国际移动设备识别码、唯一设备标识符、网络设备硬件地址、IP 地址、软件版本号、网络接入方式、状态、网络质量数据。\n"" +
                    ""b.为了预防恶意程序、确保运营质量及效率，我们会收集安装的应用信息或正在运行的进程信息、应用程序的总体运行、使用情况与频率、应用崩溃情况、总体安装使用情况、性能数据、应用来源。\n"" +
                    ""c.我们可能使用您的帐户信息、设备信息、服务日志信息以及我们关联公司、合作方在获得您授权或依法可以共享的信息，用于判断帐户安全、进行身份验证、检测及防范安全事件。\n"" +
                    ""1.3 收集、使用个人信息目的变更\n"" +
                    ""请您了解，随着我们业务的发展，可能会对提供的服务有所调整变化。原则上，当新功能或服务与我们当前提供的功能或服务相关时，收集与使用的个人信息将与原处理目的具有直接或合理关联。在与原处理目的无直接或合理关联的场景下，我们收集、使用您的个人信息，会再次进行告知，并征得您的同意。\n"" +
                    ""1.4 依法豁免征得同意收集和使用的个人信息\n"" +
                    ""请您理解，在下列情形中，根据法律法规及相关国家标准，我们收集和使用您的个人信息无需征得您的授权同意：\n"" +
                    ""a.与国家安全、国防安全直接相关的；\n"" +
                    ""b.与公共安全、公共卫生、重大公共利益直接相关的；\n"" +
                    ""c.与犯罪侦查、起诉、审判和判决执行等直接相关的；\n"" +
                    ""d.出于维护个人信息主体或其他个人的生命、财产等重大合法权益但又很难得到本人同意的；\n"" +
                    ""e.所收集的您的个人信息是您自行向社会公众公开的；\n"" +
                    ""f.从合法公开披露的信息中收集的您的个人信息的，如合法的新闻报道、政府信息公开等渠道；\n"" +
                    ""g.根据您的要求签订或履行合同所必需的；\n"" +
                    ""h.用于维护软件及相关服务的安全稳定运行所必需的，例如发现、处置软件及相关服务的故障；\n"" +
                    ""i.为合法的新闻报道所必需的；\n"" +
                    ""j.学术研究机构基于公共利益开展统计或学术研究所必要，且对外提供学术研究或描述的结果时，对结果中所包含的个人信息进行去标识化处理的；\n"" +
                    ""k.法律法规规定的其他情形。\n"" +
                    ""特别提示您注意，如信息无法单独或结合其他信息识别到您的个人身份，其不属于法律意义上您的个人信息；当您的信息可以单独或结合其他信息识别到您的个人身份时或我们将无法与任何特定个人信息建立联系的数据与其他您的个人信息结合使用时，这些信息在结合使用期间，将作为您的个人信息按照本隐私政策处理与保护。\n"" +
                    ""\n"" +
                    ""1.5 软件所收集信息说明：\n"" +
                    ""\n"" +
                    "" 数据类型：位置 Location 粗略位置 Coarse Location\n"" +
                    "" 使用者：字节跳动SDK，Gromore SDK，优量汇SDK，快手联盟SDK，友盟SDK\n"" +
                    "" 使用目的：第三方广告：我们将收集粗略位置信息，用于广告投放与监测归因。\n"" +
                    "" 是否与身份关联： 收集数据的主要目的是用于“第三方广告：显示广告”，在获得用户授权的情况下，才与设备ID、标识符相关联。\n"" +
                    "" 用于何种追踪目的：收集数据的主要目的是用于“第三方广告：显示广告”。\n"" +
                    ""\n"" +
                    "" 数据类型：标识符：Identifiers 用户ID User ID 设备ID Device ID\n"" +
                    "" 使用者：字节跳动SDK，Gromore SDK，优量汇SDK，快手联盟SDK，友盟SDK\n"" +
                    "" 使用目的：第三方广告：当用户授权应用使用标识符时，我们将收集广告标识符用于广告投放与监测归因，向您提供帮助调整广告变现策略的服务。\n"" +
                    "" 是否与身份关联： 收集数据的主要目的是用于“第三方广告：显示广告”，在获得用户授权的情况下，才与设备ID、标识符相关联。\n"" +
                    "" 用于何种追踪目的：收集数据的主要目的是用于“第三方广告：显示广告”。\n"" +
                    ""\n"" +
                    "" 数据类型：使用数据 Usage Data 产品交互 Product Interaction 广告数据 Advertising Data\n"" +
                    "" 使用者：字节跳动SDK，Gromore SDK，优量汇SDK，快手联盟SDK，友盟SDK\n"" +
                    "" 使用目的：第三方广告：我们将统计展示 、点击 、转化广告数据，以用于广告主统计投放结果，向您提供帮助调整广告变现策略的服务。\n"" +
                    "" 是否与身份关联：收集数据的主要目的是用于“第三方广告：显示广告”，在获得用户授权的情况下，才与设备ID、标识符相关联。\n"" +
                    "" 用于何种追踪目的：收集数据的主要目的是用于“第三方广告：显示广告”。\n"" +
                    ""\n"" +
                    "" 数据类型：诊断 Diagnostics 崩溃数据 Crash Data 性能数据 Performance Data\n"" +
                    "" 使用者：友盟SDK\n"" +
                    "" 使用目的：App功能：我们将收集运行过程中的崩溃信息、性能数据，最大程度减少App崩溃、确保服务器正常运行、提升可扩展性和性能。\n"" +
                    "" 是否与身份关联：收集数据的主要目的是用于“App功能：减少崩溃、确保服务器正常运行、提升可扩展性和性能等”，在获得用户授权的情况下，才与设备ID相关联。\n"" +
                    "" 用于何种追踪目的：收集数据的主要目的是用于“App功能：减少崩溃、确保服务器正常运行、提升可扩展性和性能等”。\n"" +
                    ""\n"" +
                    "" 数据类型：获取应用安装列表\n"" +
                    "" 使用者：字节跳动SDK，Gromore SDK，优量汇SDK，快手联盟SDK，友盟SDK\n"" +
                    "" 使用目的：为帮助我们更好地了解相关服务的运行情况，以便确保运行与提供服务的安全，我们可能记录网络日志信息，以及使用软件及相关服务的频率，总体安装、使用情况、性能数据等信息。\n"" +
                    "" 是否与身份关联：收集数据的主要目的是用于“第三方广告-显示广告”，在获得用户授权的情况下，才与设备ID、标识符相关联。\n"" +
                    "" 用于何种追踪目的：收集数据的主要目的是用于“第三方广告-显示广告”。\n"" +
                    ""\n"" +
                    "" 数据类型：其他数据 Other Data 其他数据类型 Other Data Types \n"" +
                    "" 使用者：字节跳动SDK，Gromore SDK，优量汇SDK，快手联盟SDK，友盟SDK\n"" +
                    "" 使用目的：第三方广告：技术上我们将收集一些设备信息（例如，系统语言、屏幕高宽、屏幕方向、屏幕DPI信息等）。\n"" +
                    "" 是否与身份关联：收集数据的主要目的是用于“第三方广告-显示广告”，在获得用户授权的情况下，才与设备ID、标识符相关联。\n"" +
                    "" 用于何种追踪目的：收集数据的主要目的是用于“第三方广告-显示广告”。\n"" +
                    ""\n"" +

                    ""2.我们如何使用Cookie等同类技术\n"" +
                    ""Cookie 和设备信息标识等同类技术是互联网中普遍使用的技术。当您使用游戏及相关服务时，我们可能会使用相关技术向您的设备发送一个或多个Cookie 或匿名标识符，以收集、标识您访问、使用本产品时的信息。我们承诺，不会将Cookie 用于本隐私政策所述目的之外的任何其他用途。我们使用Cookie 和同类技术主要为了实现以下功能或服务：\n"" +
                    ""2.1 保障产品与服务的安全、高效运转\n"" +
                    ""我们可能会设置认证与保障安全性的Cookie 或匿名标识符，使我们确认您是否安全登录服务，或者是否遇到盗用、欺诈及其他不法行为。这些技术还会帮助我们改进服务效率，提升登录和响应速度。\n"" +
                    ""2.2 帮助您获得更轻松的访问体验\n"" +
                    ""使用此类技术可以帮助您省去重复您填写个人信息、输入搜索内容的步骤和流程（示例：记录搜索、表单填写）。\n"" +
                    ""2.3 Cookie的清除\n"" +
                    ""大多数浏览器均为用户提供了清除浏览器缓存数据的功能，您可以在浏览器设置功能中进行相应的数据清除操作。如您进行清除，您可能无法使用由我们提供的、依赖于Cookie的服务或相应功能。\n"" +
                    ""\n"" +

                    ""3.我们如何共享、转让、公开披露个人信息\n"" +
                    ""3.1 个人信息的共享、转让\n"" +
                    ""我们不会向第三方共享、转让您的个人信息，除非经过您本人事先授权同意，或者共享、转让的个人信息是去标识化处理后的信息，且共享第三方无法重新识别此类信息的自然人主体\n"" +
                    ""3.1.1我们可能会共享的个人信息\n"" +
                    ""a.为实现相关功能或服务与关联方共享\n"" +
                    ""b.当您在使用游戏及相关服务时，为保障您在我们及关联方提供的产品间所接受服务的一致性，并方便统一管理您的信息，我们会将您去标识化后的个人信息与这些关联方共享。\n"" +
//                  ""c.为了提升您的多平台互通游戏体验，在您以同一抖音帐号/手机号登录并使用游戏名称中提供的游戏以及该游戏的独立APP安卓版本时，经您授权同意，我们可能会与该游戏共享您的账号信息、该游戏中的行为日志、游戏进度等信息以实现游戏数据的互通。\n"" +
                    ""3.1.2 对共享个人信息第三方主体的谨慎评估及责任约束\n"" +
                    ""a.经您同意，我们只会与第三方共享实现目的所必要的信息。如果第三方因业务需要，确需超出前述授权范围使用个人信息的，该第三方将需再次征求您的同意。\n"" +
                    ""b. 对我们与之共享您个人信息的第三方，我们将审慎评估该第三方数据使用共享信息的目的，对其安全保障能力进行综合评估，并要求其遵循合作法律协议。我们会对合作方获取信息的软件工具开发包（SDK）、应用程序接口（API）进行严格的安全监测，以保护数据安全。\n"" +
                    ""3.1.3 收购、兼并、重组时个人信息的转让\n"" +
                    ""随着我们业务的持续发展，我们将有可能进行合并、收购、资产转让，您的个人信息有可能因此而被转移。在发生前述变更时，我们将按照法律法规及不低于本隐私政策所要求的安全标准继续保护或要求个人信息的继受方继续保护您的个人信息，否则我们将要求继受方重新征得您的授权同意。\n"" +
                    ""3.2 个人信息的公开披露\n"" +
                    ""我们仅会在以下情况下，且采取符合业界标准的安全防护措施的前提下，才会披露您的个人信息：\n"" +
                    ""3.2.1 根据您的需求，在您明确同意的披露方式下披露您所指定的信息；\n"" +
                    ""3.2.2 对违规账号、欺诈行为进行处罚公告时，我们会披露相关账号的必要信息。\n"" +
                    ""3.3 依法豁免征得同意共享、转让、公开披露的个人信息\n"" +
                    ""请您理解，在下列情形中，根据法律法规及国家标准，我们共享、转让、公开披露您的个人信息无需征得您的授权同意：\n"" +
                    ""（1）与国家安全、国防安全直接相关的；\n"" +
                    ""（2）与公共安全、公共卫生、重大公共利益直接相关的；\n"" +
                    ""（3）与犯罪侦查、起诉、审判和判决执行等直接相关的\n"" +
                    ""（4）出于维护您或其他个人的生命、财产等重大合法权益但又很难得到本人同意的；\n"" +
                    ""（5）您自行向社会公众公开的个人信息；\n"" +
                    ""（6）从合法公开披露的信息中收集个人信息的，如合法的新闻报道、政府信息公开等渠道。\n"" +
                    ""根据法律规定，共享、转让、披露经去标识化处理的个人信息，且确保数据接收方无法复原并重新识别信息主体的，不属于个人信息的对外共享、转让及公开披露行为，对此类数据的保存及处理将无需另行向您通知并征得您的同意。\n"" +
                    ""\n"" +

                    ""4、我们如何存储个人信息\n"" +
                    ""4.1 存储地点\n"" +
                    ""我们依照法律法规的规定，将在境内运营过程中收集和产生的您的个人信息存储于中华人民共和国境内。目前，我们不会将上述信息传输至境外，如果我们向境外传输，我们将会遵循相关国家规定或者征求您的同意。\n"" +
                    ""4.2 存储期限\n"" +
                    ""我们仅在为提供游戏及服务之目的所必需的期间内保留您的个人信息：在您未撤回、删除或未注销帐号期间，我们会保留相关信息。超出必要期限后，我们将对您的个人信息进行删除或匿名化处理，但法律法规另有规定的除外。\n"" +
                    ""\n"" +

                    ""5.我们如何保护个人信息的安全\n"" +
                    ""5.1 我们非常重视您个人信息的安全，将努力采取合理的安全措施（包括技术方面和管理方面）来保护您的个人信息，防止您提供的个人信息被不当使用或在未经授权的情况下被访问、公开披露、使用、修改、损坏、丢失或泄漏。\n"" +
                    ""5.2 我们会使用不低于行业同行的加密技术、匿名化处理及相关合理可行的手段保护您的个人信息，并使用安全保护机制防止您的个人信息遭到恶意攻击。\n"" +
                    ""5.3 我们会建立专门的安全部门、安全管理制度、数据安全流程保障您的个人信息安全。我们采取严格的数据使用和访问制度，确保只有授权人员才可访问您的个人信息，并适时对数据和技术进行安全审计。\n"" +
                    ""5.4 尽管已经采取了上述合理有效措施，并已经遵守了相关法律规定要求的标准，但请您理解，由于技术的限制以及可能存在的各种恶意手段，在互联网行业，即便竭尽所能加强安全措施，也不可能始终保证信息百分之百的安全，我们将尽力确保您提供给我们的个人信息的安全性。\n"" +
                    ""5.5 我们会制定应急处理预案，并在发生用户信息安全事件时立即启动应急预案，努力阻止这些安全事件的影响和后果扩大。一旦发生用户信息安全事件（泄露、丢失）后，我们将按照法律法规的要求，及时向您告知：安全事件的基本情况和可能的影响、我们已经采取或将要采取的处置措施、您可自主防范和降低风险的建议、对您的补救措施。我们将及时将事件相关情况以推送通知、邮件、信函、短信及相关形式告知您，难以逐一告知时，我们会采取合理、有效的方式发布公告。同时，我们还将按照相关监管部门要求，上报用户信息安全事件的处置情况。\n"" +
                    ""5.6 我们谨此特别提醒您，本隐私政策提供的个人信息保护措施仅适用于“{APP_NAME}”软件及相关服务。您一旦离开“{APP_NAME}”及相关服务，浏览或使用其他网站、服务及内容资源，我们将没有能力和直接义务保护您在“{APP_NAME}”及相关服务之外的软件、网站提交的任何个人信息，无论您登录、浏览或使用上述软件、网站是否基于“{APP_NAME}”的链接或引导。\n"" +
                    ""\n"" +

                    ""6.您的权利\n"" +
                    ""我们非常重视您对个人信息的管理，并尽全力保护您对于您个人信息的查询、访问、修改、删除、撤回同意授权、注销帐号、投诉举报以及设置隐私功能等权利，以使您有能力保障您的隐私和信息安全。\n"" +
                    ""6.1 访问、删除、更正您的个人信息\n"" +
                    ""在您使用本服务期间，我们可能会视产品具体情况为您提供相应的操作设置，以便您可以访问、删除、更正您的相关个人信息。\n"" +
                    ""特别提示您注意，出于安全性和身份识别（如号码申诉服务）的考虑，您可能无法自主修改注册时提交的某些初始注册信息。如您确有需要修改该类注册信息，您可以通过 {APP_EMAIL} 与我们进行联系。\n"" +
                    ""6.2 改变您授权同意范围或撤销授权\n"" +
                    ""您可以在设备本身操作系统中关闭相关敏感权限改变同意范围或撤回您的授权。\n"" +
                    ""请您理解，特定的业务功能和服务将需要您的信息才能得以完成，当您撤回同意或授权后，我们无法继续为您提供撤回同意或授权所对应的功能和服务，也不再处理您相应的个人信息。但您撤回同意或授权的决定，不会影响我们此前基于您的授权而开展的个人信息处理。\n"" +
                    ""6.3 注销帐号\n"" +
                    ""如果您不再希望使用我们的服务，您可以通过向我们发送邮件的（{APP_EMAIL}）的方式申请注销账号。在您注销账号前，我们将验证您的个人身份、安全状态、设备信息等。请您知悉并理解，注销账号的行为是不可逆的行为，当您注销账号后，我们将删除有关您的相关信息，但法律法规另有规定的除外。\n"" +
                    ""6.4 投诉举报\n"" +
                    ""您可以按照我们公示的制度进行投诉或举报。如果您认为您的个人信息权利可能受到侵害，或者发现侵害个人信息权利的线索，您可以通过 {APP_EMAIL} 与我们进行联系， 我们核查后会在30日内反馈您的投诉与举报。\n"" +
                    ""6.5 访问隐私政策\n"" +
                    ""您可以在注册登录页面，查看本隐私政策的全部内容或在“{APP_NAME}”软件内查看本隐私政策全部内容。\n"" +
                    ""6.6 停止运营向您告知\n"" +
                    ""\n"" +
                    ""如我们停止运营，我们将及时停止收集您个人信息的活动，将停止运营的通知以逐一送达或公告的形式通知您，并对所持有的您的个人信息进行删除或匿名化处理。\n"" +
                    ""\n"" +

                    ""7.未成年人条款\n"" +
                    ""7.1 若您是未满18周岁的未成年人，在使用“{APP_NAME}”软件及相关服务前，应在您的父母或其他监护人监护、指导下共同阅读并同意本隐私政策。\n"" +
                    ""7.2 我们会积极按照国家防沉迷政策要求，通过启用防沉迷系统保护未成年人的合法权益。我们会通过实名身份等信息校验判断相关账号的实名信息是否为未成年人，进而决定是否将此账号纳入到防沉迷体系中。另外，我们会收集您的登录时间、游戏时长等信息，通过从系统层面自动干预和限制未成年人游戏时间、启用强制下线功能等方式，引导未成年人合理游戏，并在疑似未成年人消费后尝试联系其监护人进行提醒、确认与处理，帮助未成年人健康上网。\n"" +
                    ""7.3 我们根据国家相关法律法规的规定保护未成年人的个人信息，只会在法律允许、父母或其他监护人明确同意或保护未成年人所必要的情况下收集、使用、储存、共享、转让或披露未成年人的个人信息；如果我们发现在未事先获得可证实的父母同意的情况下收集了未成年人的个人信息，则会设法尽快删除相关信息。\n"" +
                    ""7.4 若您是未成年人的监护人，当您对您所监护的未成年人的个人信息有相关疑问时，请通过公司本隐私政策公示的联系方式与我们联系。\n"" +
                    ""\n"" +

                    ""8.隐私政策的修订和通知\n"" +
                    ""8.1 为了给您提供更好的服务，“{APP_NAME}”及相关服务将不时更新与变化，我们会适时对本隐私政策进行修订，这些修订构成本隐私政策的一部分并具有等同于本隐私政策的效力。\n"" +
                    ""8.2 本隐私政策更新后，我们会在“{APP_NAME}”发出更新版本，并在更新后的条款生效前通过应用程序更新公告或其他适当的方式提醒您更新的内容，以便您及时了解本隐私政策的最新版本。\n"" +
                    ""\n"" +

                    ""9.联系我们\n"" +
                    ""9.1 如果您对个人信息保护问题有投诉、建议、疑问，您可以将问题发送至\n"" +
                    ""{APP_EMAIL}，我们核查并验证您的用户身份后会及时反馈您的投诉与举报。\n"" +
                    """"
            );

            dialog.setCancelable(false);  //是否可以取消
            dialog.setNegativeButton(""拒绝"", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialogInterface, int i) {
                    dialogInterface.dismiss();
                    android.os.Process.killProcess(android.os.Process.myPid());
                }
            });

            dialog.setPositiveButton(""同意"", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    SharedPreferences.Editor editor = base.edit();
                    editor.putBoolean(""PrivacyFlag"", false);
                    editor.commit();
                }
            });
            // dialog.show().getWindow().setLayout(1000, 1300);
            dialog.show();
        }
    ";
}

#endif
