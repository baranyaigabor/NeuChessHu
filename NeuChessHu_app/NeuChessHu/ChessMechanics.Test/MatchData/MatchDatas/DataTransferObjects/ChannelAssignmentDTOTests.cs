using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using NUnit.Framework;

namespace ChessMechanics.Test.MatchData.MatchDatas.DataTransferObjects;

[TestFixture]
public class ChannelAssignmentDTOTests
{
    [Test]
    public void ConstructorWithValuesStoresChannelAndPlayerID()
    {
        ChannelAssignmentDTO dto = new("channel", "22");

        Assert.Multiple(() =>
        {
            Assert.That(dto.Channel, Is.EqualTo("channel"));
            Assert.That(dto.PlayerID, Is.EqualTo("22"));
        });
    }
}