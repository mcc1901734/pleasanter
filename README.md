# Implem.Pleasanter
## 概要
.NET製のアプリケーションPaaS。現場のマネジメントを快適化することで、生産性を向上し続ける仕組みを実現。商用、非商用を問わず無料で使えるオープンソース・ソフトウェア。  

オープンソースのアプリケーションPaaSというジャンルでデファクトスタンダードを目指します。プルリクエスト大歓迎。ご興味ある方はお気軽にご連絡ください。  
## 機能概要
プリザンターの機能の概要をアニメーションGIFを用いて説明します。  
http://qiita.com/Implem/items/39ef5be388f40aa21c04

## 利用ガイド
プリザンターのセットアップ手順及び、操作手順を説明します。  
https://github.com/Implem/Implem.Pleasanter/wiki

## デモサイト
下記のサイトでe-mailアドレスを登録するとプリザンターを試行することが出来ます。  
https://pleasanter.azurewebsites.net

## ダウンロード
セットアップモジュールのダウンロードサイトです。  
http://pleasanter.org

## ブログ
プリザンターの活用方法を紹介するブログです。  
https://implem.co.jp/category/blog/

## 動作イメージ
* テーブル管理
![default](https://cloud.githubusercontent.com/assets/12204265/19873886/e25c990e-a004-11e6-8e74-4d157e5fc2db.gif)

* ガントチャート
![default](https://cloud.githubusercontent.com/assets/12204265/19873780/4a8411a2-a004-11e6-960a-0af292cb27ee.gif)

* バーンダウンチャート
![default](https://cloud.githubusercontent.com/assets/12204265/19873800/6b917fd8-a004-11e6-87f4-4cc1aa6b1d20.gif)

* 時系列チャート
![default](https://cloud.githubusercontent.com/assets/12204265/19873846/a7f5469e-a004-11e6-9fd7-1772abc4cafd.gif)

* カンバン
![default](https://cloud.githubusercontent.com/assets/12204265/19873860/c35f79f4-a004-11e6-801b-6d3463e34201.gif)

## 動作条件
|条件|Windows Server 2012 R2 / 2016|Windows 10|Microsoft Azure|
|:--|:--:|:--:|:--:|
|.NET Framework 4.5|導入済|導入済|-|
|IIS|◯|◯|-|
|ASP.NET 4.5|◯|◯|-|
|SQL Server 2012/2014/2016|◯|◯|-|
|Web Deploy v3.5|◯|◯|-|
|Microsoft Azure Web App|-|-|◯|
|Microsoft Azure SQL Database|-|-|◯|

## 機能概要
| 項目               | 説明                                  |
|:-------------------|:--------------------------------------|
|[サイトメニュー](https://github.com/Implem/Implem.Pleasanter/wiki/サイト機能：サイトメニュー)|ファイルサーバのような階層構造|
|[サイト](https://github.com/Implem/Implem.Pleasanter/wiki/サイト機能：サイト)|情報の入れ物|
|[サイト設定](https://github.com/Implem/Implem.Pleasanter/wiki/サイト機能：サイト設定)|サイトのカスタマイズ|
|[期限付きテーブル](https://github.com/Implem/Implem.Pleasanter/wiki/サイト機能：期限付きテーブル)|タスク管理など期限のあるデータを表形式で管理するテーブルの入れ物|
|[記録テーブル](https://github.com/Implem/Implem.Pleasanter/wiki/サイト機能：記録テーブル)|顧客リストなど期限の無いデータを表形式で管理するテーブルの入れ物|
|[Wiki](https://github.com/Implem/Implem.Pleasanter/wiki/サイト機能：Wiki)|マークダウン記法に対応したマニュアルやリンク集のページ|
|[カスタム項目](https://github.com/Implem/Implem.Pleasanter/wiki#%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%A0%E9%A0%85%E7%9B%AE)|テーブルのカスタマイズ可能な入力フィールドの設定|
|[フィルタ](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：アウトプット：フィルタ)|テーブルのフィルタリング|
|[ソータ](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：アウトプット：ソータ)|一覧の並び替え|
|[ビューモード](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：アウトプット：ビューモード)|テーブルの表示形式を一覧、ガントチャート、バーンダウンチャート、時系列チャート、カンバンに切り替え|
|[レコード一覧](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：アウトプット：レコード一覧)|レコードの一覧表示|
|[カレンダー](https://github.com/Implem/Implem.Pleasanter/wiki/ビューモードの種類：カレンダー)|テーブルのカレンダー表示|
|[クロス集計](https://github.com/Implem/Implem.Pleasanter/wiki/ビューモードの種類：クロス集計)|テーブルのクロス集計表示|
|[ガントチャート](https://github.com/Implem/Implem.Pleasanter/wiki/ビューモードの種類：ガントチャート)|テーブルのガントチャート表示|
|[バーンダウンチャート](https://github.com/Implem/Implem.Pleasanter/wiki/ビューモードの種類：バーンダウンチャート)|テーブルのバーンダウンチャート表示|
|[時系列チャート](https://github.com/Implem/Implem.Pleasanter/wiki/ビューモードの種類：時系列チャート)|テーブルのテーブルの件数または数値フィールドの合計、平均、最大、最小を面グラフで表示|
|[カンバン](https://github.com/Implem/Implem.Pleasanter/wiki/ビューモードの種類：カンバン)|テーブルの状況やカスタムフィールドの分類をカンバン表示|
|[集計](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：アウトプット：集計)|テーブルの件数または数値フィールドの合計、平均、最大、最小を分類毎に集計して表示|
|[リンク](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：ビジネスロジック：リンク)|テーブルでサイト間のテーブルの親子関係を設定|
|[サマリ](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：ビジネスロジック：サマリ)|リンクしているテーブルの件数または数値フィールドの合計、平均、最大、最小をカスタムフィールドに格納|
|[計算式](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：ビジネスロジック：計算式)|テーブルで四則演算の結果をカスタムフィールドに格納|
|[通知](https://github.com/Implem/Implem.Pleasanter/wiki/データ管理：アウトプット：通知)|テーブルでテーブルの追加、変更、削除をSlackまたはメールで通知|
|[インポート]()|テーブルをCSVファイルからインポート|
|[エクスポート]()|テーブルをCSVファイルにエクスポート|
|[メール]()|テーブルからメールを送信|
|[コメント]()|テーブルにコメントを追加|
|[分割]()|テーブルを複数のテーブルに分割|
|[バージョン]()|テーブルの更新履歴の保存と参照|
|[スクリプト]()|カスタムJavaScript|
|[スタイル]()|カスタムCSS|
|[キーワード検索]()|テーブルやWikiの横断検索|
|[マークダウン]()|マークダウン記法でテキストをスタイリング|
|[アクセス制御]()|サイト単位に利用者の権限を設定|
|[認証]()|ローカル認証、LDAP認証|
|[マルチ言語]()|日英（拡張可能）|

## Auther
Implem Inc.  
<https://implem.co.jp>
