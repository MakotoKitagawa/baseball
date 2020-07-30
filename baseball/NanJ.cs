using System;
using System.Collections.Generic;
using System.Text;

namespace baseball
{
    class NanJ
    {
        Random rnd = new Random();
        Score score = new Score();

        public string[] yakiuPositiveB { get; set; } = new string[]
        {
            
            "彡(^)(^)「ああ＾～」",
            "彡(^)(^)「【速報】男村田緊急来日」",
            "彡(^)(^)「これは球界の至宝」",
            "彡(^)(^)「サンキューガッツ」",
            "彡(^)(^)「すごE」",
            "彡(^)(^)「ええの獲ったわ！」",
            "彡(^)(^)「これは日本の主砲」",
            "彡(^)(^)「なんてすごいんだ……(恍惚)」",
            "彡(^)(^)「あれは……バースの再来？！」",
            "彡(^)(^)「あれは……プリンセスフォーム若月？！」",
            "(*^◯^*)「勝てる……勝てるんだ！」",

        };

        public string[] yakiuNegativeB { get; set; } = new string[]
       {
            "彡(ﾟ)(ﾟ)「アカン」",
            "彡(ﾟ)(ﾟ)「（次に）切り替えていく」",
            "彡(ﾟ)(ﾟ)「あ　ほ　く　さ」",
            "彡(ﾟ)(ﾟ)「辞めたらこの仕事～」",
            "彡(ﾟ)(ﾟ)「頭を割って、中を見てみたい」",
            "彡(ﾟ)(ﾟ)「チャンスGやん」",
            "彡(ﾟ)(ﾟ)「新井が悪いよ新井が～」",
            "彡(ﾟ)(ﾟ)「は～……つっかえ！」",
            "彡(ﾟ)(ﾟ)「いかんでしょ」",
       };

        public string[] yakiuPositiveP { get; set; } = new string[]
      {
            "彡(^)(^)「球がHOP-UPしてないか？」",
            "彡(^)(^)「おかしなことやっとる」",
            "彡(^)(^)「あったよ！守護神！」",
            "彡(^)(^)「どうやって打つんだよこの化け物」",
            "彡(^)(^)「信じとったで！」",
            "彡(^)(^)「負ける気せぇへん地元やし」",
            "彡(^)(^)「いけるやん！」",
            "(*^◯^*)「横浜にもこんな選手がいるんだ！」",

      };

        public string[] yakiuNegativeP { get; set; } = new string[]
      {

            "彡(ﾟ)(ﾟ)「一緒や！打っても！」",
            "彡(ﾟ)(ﾟ)「これは大変な事やと思うよ」",
            "彡(ﾟ)(ﾟ)「絶対に許さない。顔も見たくない」",
            "彡(ﾟ)(ﾟ)「どうすれば勝てるのか分かってない」",
            "彡(ﾟ)(ﾟ)「なにわろてんねん」",
            "彡(ﾟ)(ﾟ)「ヒエ～ッｗｗｗｗｗｗｗ」",
            "彡(ﾟ)(ﾟ)「うーんこれはベイス★ボール！ｗ」",
            "彡(ﾟ)(ﾟ)「もう許さねぇからなぁ？」",
      };

        public string[] yakiuHomerunP{ get; set; } = new string[]
       {
            "彡(^)(^)「痛烈！一閃！！」",
            "彡(^)(^)「Vやねん！」",
            "彡(^)(^)「いったあああああああああああああ！！」",


       };

        public string[] yakiuHomerunN { get; set; } = new string[]
       {
            "彡(ﾟ)(ﾟ)「ファーーーーーーｗｗｗｗｗｗｗ」",
            "彡(ﾟ)(ﾟ)「【悲報】投手、飛翔」",
            "彡(ﾟ)(ﾟ)「悪夢のような現実がそこには待っていました」",
            "彡(ﾟ)(ﾟ)「あああああああああああああああああああああああああああああああ！！！！！！！！！！！（ﾌﾞﾘﾌﾞﾘﾌﾞﾘﾌﾞﾘｭﾘｭﾘｭﾘｭﾘｭﾘｭ！！！！！！ﾌﾞﾂﾁﾁﾌﾞﾌﾞﾌﾞﾁﾁﾁﾁﾌﾞﾘﾘｲﾘﾌﾞﾌﾞﾌﾞﾌﾞｩｩｩｩｯｯｯ！！！！！！！ ）」",

       };

        public void NanJpojiButter()
        {
            int i = rnd.Next(0, 11);
            Console.WriteLine(yakiuPositiveB[i]);
        }

        public void NanJnegaButter()
        {
            int i = rnd.Next(0, 9);
            Console.WriteLine(yakiuNegativeB[i]);
        }

        public void NanJpojiPitcher()
        {
            int i = rnd.Next(0, 8);
            Console.WriteLine(yakiuPositiveP[i]);
        }

        public void NanJnegaPitcher()
        {
            int i = rnd.Next(0, 8);
            Console.WriteLine(yakiuNegativeP[i]);
        }

        public void NanJpojiHomerun()
        {
            int i = rnd.Next(0, 3);
            Console.WriteLine("彡(ﾟ)(ﾟ)「お……お……？」");
            score.loading();
            Console.WriteLine("HOOOOOOOOOMRUUUUUUUUNNNNN!!!!!!");
            Console.WriteLine(yakiuHomerunP[i]);
        }

        public void NanJnegaHomerun()
        {
            int i = rnd.Next(0, 4);
            Console.WriteLine("彡(ﾟ)(ﾟ)「行くな！行くな！超えるな！！」");
            score.loading();
            Console.WriteLine("HOOOOOOOOOMRUUUUUUUUNNNNN!!!!!!");
            Console.WriteLine(yakiuHomerunN[i]);
        }
    }
}
