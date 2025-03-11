using ActorReposLib;

namespace ActorsREST.Records
{
    public record ActorRecord(int? Id, string? Name, int? Birthyear);

    public static class RecordHelper
    {
        public static Actor ConvertActorRecord(ActorRecord record)
        {
            if (record.Id == null) { throw new ArgumentNullException("" + record.Id); }
            if (record.Birthyear == null) { throw new ArgumentNullException("" + record.Birthyear); }

            return new Actor() { BirthYear = (int)record.Birthyear, Id = (int)record.Id, Name = record.Name };
        }
    }
    
}
