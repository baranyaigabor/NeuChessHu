using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using NUnit.Framework;

namespace ChessMechanics.Tests.MatchData.MatchDatas.DataTransferObjects;

[TestFixture]
public class InitializerDTOTests
{
    [Test]
    public void ConstructorWithValuesStoresInitializerFields()
    {
        MatchStateDTO state = new() { MatchID = "match" };
        ClocksDTO clocks = new() { WhiteRemainingMs = 1, BlackRemainingMs = 2 };

        InitializerDTO dto = new("10", "11", state, clocks);

        Assert.Multiple(() =>
        {
            Assert.That(dto.WhiteID, Is.EqualTo("10"));
            Assert.That(dto.BlackID, Is.EqualTo("11"));
            Assert.That(dto.InitialState, Is.SameAs(state));
            Assert.That(dto.Clocks, Is.SameAs(clocks));
        });
    }
}
