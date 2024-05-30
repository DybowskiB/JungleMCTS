using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.Tests
{
    public class CaptureControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldCapture()
        {
            // First player pieces
            Mouse mouse1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Cat cat1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Dog dog1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Wolf wolf1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Cheetah cheetah1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Tiger tiger1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Lion lion1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Elephant elephant1 = new(Enums.PlayerIdEnum.FirstPlayer);

            // Second player pieces
            Mouse mouse2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Cat cat2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Dog dog2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Wolf wolf2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Cheetah cheetah2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Tiger tiger2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Lion lion2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Elephant elephant2 = new(Enums.PlayerIdEnum.SecondPlayer);

            // Lists of pieces
            List<Piece> player1Pieces = [mouse1, cat1, dog1, wolf1, cheetah1, tiger1, lion1, elephant1];
            List<Piece> player2Pieces = [mouse2, cat2, dog2, wolf2, cheetah2, tiger2, lion2, elephant2];

            // Fields
            DefaultField defaultField = new();
            Lake lake = new();
            Trap trap1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Trap trap2 = new(Enums.PlayerIdEnum.SecondPlayer);


            //
            // Capture on default fields
            //

            // First player attack
            for (int i = 0; i < player2Pieces.Count; ++i)
            {
                for (int j = i; j < player1Pieces.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            player1Pieces[j], defaultField, 
                            player2Pieces[i], defaultField),
                        Is.EqualTo(true));
                }
            }
            // Mouse attack elephant
            Assert.That(
                        CaptureController.CanCapture(
                            player1Pieces.First(), defaultField,
                            player2Pieces.Last(), defaultField),
                        Is.EqualTo(true));

            // Second player attacks
            for (int i = 0; i < player1Pieces.Count; ++i)
            {
                for (int j = i; j < player2Pieces.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            player2Pieces[j], defaultField,
                            player1Pieces[i], defaultField),
                        Is.EqualTo(true));
                }
            }
            // Mouse attack elephant
            Assert.That(
                        CaptureController.CanCapture(
                            player2Pieces.First(), defaultField,
                            player1Pieces.Last(), defaultField),
                        Is.EqualTo(true));


            //
            // Capture on lake
            //
            
            // Water pieces
            List<Piece> waterPiecesPlayer1 = [mouse1, dog1, wolf1];
            List<Piece> waterPiecesPlayer2 = [mouse2, dog2, wolf2];

            // First player attack
            for (int i = 0; i < waterPiecesPlayer2.Count; ++i)
            {
                for (int j = i; j < waterPiecesPlayer1.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            waterPiecesPlayer1[j], lake,
                            waterPiecesPlayer2[i], lake),
                        Is.EqualTo(true));
                }
            }
            // Second player attack
            for (int i = 0; i < waterPiecesPlayer1.Count; ++i)
            {
                for (int j = i; j < waterPiecesPlayer2.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            waterPiecesPlayer2[j], lake,
                            waterPiecesPlayer1[i], lake),
                        Is.EqualTo(true));
                }
            }


            //
            // Capture on trap
            //

            // First player attack
            for (int i = 0; i < player1Pieces.Count; ++i)
            {
                for (int j = 0; j < player2Pieces.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            player1Pieces[i], defaultField,
                            player2Pieces[j], trap1),
                        Is.EqualTo(true));
                }
            }
            // Second player attack
            for (int i = 0; i < player2Pieces.Count; ++i)
            {
                for (int j = i; j < player1Pieces.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            player2Pieces[j], defaultField,
                            player1Pieces[i], trap2),
                        Is.EqualTo(true));
                }
            }


            //
            // Capture from own trap
            //

            // First player attack
            for (int i = 0; i < player2Pieces.Count; ++i)
            {
                for (int j = i; j < player1Pieces.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            player1Pieces[j], trap1,
                            player2Pieces[i], defaultField),
                        Is.EqualTo(true));
                }
            }
            // Mouse attack elephant
            Assert.That(
                        CaptureController.CanCapture(
                            player1Pieces.First(), trap1,
                            player2Pieces.Last(), defaultField),
                        Is.EqualTo(true));

            // Second player attack
            for (int i = 0; i < player1Pieces.Count; ++i)
            {
                for (int j = i; j < player2Pieces.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            player2Pieces[j], trap2,
                            player1Pieces[i], defaultField),
                        Is.EqualTo(true));
                }
            }
            // Mouse attack elephant
            Assert.That(
                        CaptureController.CanCapture(
                            player2Pieces.First(), trap2,
                            player1Pieces.Last(), defaultField),
                        Is.EqualTo(true));
        }


        [Test]
        public void ShouldNotCapture()
        {
            // First player pieces
            Mouse mouse1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Cat cat1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Dog dog1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Wolf wolf1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Cheetah cheetah1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Tiger tiger1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Lion lion1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Elephant elephant1 = new(Enums.PlayerIdEnum.FirstPlayer);

            // Second player pieces
            Mouse mouse2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Cat cat2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Dog dog2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Wolf wolf2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Cheetah cheetah2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Tiger tiger2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Lion lion2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Elephant elephant2 = new(Enums.PlayerIdEnum.SecondPlayer);

            // Lists of pieces
            List<Piece> player1Pieces = [mouse1, cat1, dog1, wolf1, cheetah1, tiger1, lion1, elephant1];
            List<Piece> player2Pieces = [mouse2, cat2, dog2, wolf2, cheetah2, tiger2, lion2, elephant2];

            // Fields
            DefaultField defaultField = new();
            Lake lake = new();
            Trap trap1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Trap trap2 = new(Enums.PlayerIdEnum.SecondPlayer);


            //
            // Capture on default fields
            //

            // First player attack
            for (int i = 0; i < player2Pieces.Count; ++i)
            {
                for (int j = i - 1; j >= 0; --j)
                {
                    // Mouse attack elephant
                    if (player1Pieces[j] is Mouse && player2Pieces[i] is Elephant) continue;
                    Assert.That(
                        CaptureController.CanCapture(
                            player1Pieces[j], defaultField,
                            player2Pieces[i], defaultField),
                        Is.EqualTo(false));
                }
            }

            // Second player attacks
            for (int i = 0; i < player1Pieces.Count; ++i)
            {
                for (int j = i - 1; j >= 0; --j)
                {
                    // Mouse attack elephant
                    if (player2Pieces[j] is Mouse && player1Pieces[i] is Elephant) continue;
                    Assert.That(
                        CaptureController.CanCapture(
                            player2Pieces[j], defaultField,
                            player1Pieces[i], defaultField),
                        Is.EqualTo(false));
                }
            }


            //
            // Capture on lake
            //

            // Water pieces
            List<Piece> waterPiecesPlayer1 = [mouse1, dog1, wolf1];
            List<Piece> waterPiecesPlayer2 = [mouse2, dog2, wolf2];

            // First player attack
            for (int i = 0; i < waterPiecesPlayer2.Count; ++i)
            {
                for (int j = i - 1; j >= 0; --j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            waterPiecesPlayer1[j], lake,
                            waterPiecesPlayer2[i], lake),
                        Is.EqualTo(false));
                }
            }
            // Second player attack
            for (int i = 0; i < waterPiecesPlayer1.Count; ++i)
            {
                for (int j = i - 1; j >= 0; --j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            waterPiecesPlayer2[j], lake,
                            waterPiecesPlayer1[i], lake),
                        Is.EqualTo(false));
                }
            }



            //
            // Capture from lake
            //

            // First player attack
            for (int i = 0; i < waterPiecesPlayer1.Count; ++i)
            {
                for (int j = 0; j < player2Pieces.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            waterPiecesPlayer1[i], lake,
                            player2Pieces[j], defaultField),
                        Is.EqualTo(false));
                }
            }
            // Second player attack
            for (int i = 0; i < player1Pieces.Count; ++i)
            {
                for (int j = 0; j < waterPiecesPlayer2.Count; ++j)
                {
                    Assert.That(
                        CaptureController.CanCapture(
                            waterPiecesPlayer2[j], lake,
                            player1Pieces[i], defaultField),
                        Is.EqualTo(false));
                }
            }


            //
            // Capture from own trap
            //

            // First player attack
            for (int i = 0; i < player2Pieces.Count; ++i)
            {
                for (int j = i - 1; j >=0; --j)
                {
                    // Mouse attack elephant
                    if (player1Pieces[j] is Mouse && player2Pieces[i] is Elephant) continue;
                    Assert.That(
                        CaptureController.CanCapture(
                            player1Pieces[j], trap1,
                            player2Pieces[i], defaultField),
                        Is.EqualTo(false));
                }
            }

            // Second player attack
            for (int i = 0; i < player1Pieces.Count; ++i)
            {
                for (int j = i - 1; j >= 0; --j)
                {
                    // Mouse attack elephant
                    if (player2Pieces[j] is Mouse && player1Pieces[i] is Elephant) continue;
                    Assert.That(
                        CaptureController.CanCapture(
                            player2Pieces[j], trap2,
                            player1Pieces[i], defaultField),
                        Is.EqualTo(false));
                }
            }
        }
    }
}