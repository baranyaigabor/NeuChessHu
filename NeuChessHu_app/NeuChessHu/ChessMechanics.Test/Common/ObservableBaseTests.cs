using ChessMechanics.Common;
using NUnit.Framework;

namespace ChessMechanics.Tests.Common;

[TestFixture]
public class ObservableBaseTests
{
    [Test]
    public void RaisePropertyChangedWithoutNameUsesCallerMemberName()
    {
        ObservableModel model = new();
        string? raisedName = null;

        model.PropertyChanged += (_, e) => raisedName = e.PropertyName;

        model.Name = "new value";

        Assert.That(raisedName, Is.EqualTo(nameof(ObservableModel.Name)));
    }

    [Test]
    public void RaisePropertyChangedWithExplicitNameUsesProvidedName()
    {
        ObservableModel model = new();
        string? raisedName = null;

        model.PropertyChanged += (_, e) => raisedName = e.PropertyName;

        model.RaiseExplicit("ManualProperty");

        Assert.That(raisedName, Is.EqualTo("ManualProperty"));
    }

    class ObservableModel : ObservableBase
    {
        string? name;

        public string? Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        public void RaiseExplicit(string propertyName) => 
            RaisePropertyChanged(propertyName);
    }
}