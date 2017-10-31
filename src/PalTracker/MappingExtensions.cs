﻿using PalTracker.Data;

namespace PalTracker
{
    public static class MappingExtensions
    {
        public static TimeEntry toEntity(this TimeEntryRecord record) => new TimeEntry
        {
            Id = record.Id,
            ProjectId = record.ProjectId,
            UserId = record.UserId,
            Date = record.Date,
            Hours = record.Hours
        };

        public static TimeEntryRecord toRecord(this TimeEntry entity) => new TimeEntryRecord
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            UserId = entity.UserId,
            Date = entity.Date,
            Hours = entity.Hours
        };
    }
}