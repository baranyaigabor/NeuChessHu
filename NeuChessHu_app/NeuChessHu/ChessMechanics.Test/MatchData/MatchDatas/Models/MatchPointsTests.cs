using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Patching;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using NUnit.Framework;

namespace ChessMechanics.Test.MatchData.MatchDatas.Models;

[TestFixture]
public class MatchPointsTests
{
    [Test]
    public void MatchPointsReasonWhenPatchedRaisesPropertyChanged()
    {
        MatchPoints matchPoints = new();

        string? propertyName = null;
        matchPoints.PropertyChanged += (_, e) => propertyName = e.PropertyName;

        Patcher.PatchMatchPoints(new MatchPointsDTO { MatchPointsReason = "Resignation" }, matchPoints);

        Assert.That(propertyName, Is.EqualTo(nameof(MatchPoints.MatchPointsReason)));
    }

    [Test]
    public void WinnerIDWhenPatchedRaisesPropertyChanged()
    {
        MatchPoints matchPoints = new();

        string? propertyName = null;
        matchPoints.PropertyChanged += (_, e) => propertyName = e.PropertyName;

        Patcher.PatchMatchPoints(new MatchPointsDTO { WinnerID = 99 }, matchPoints);

        Assert.That(propertyName, Is.EqualTo(nameof(MatchPoints.WinnerID)));
    }
}