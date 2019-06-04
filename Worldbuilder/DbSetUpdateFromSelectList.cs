using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder
{
    public enum Relationship { Straight, Reversed }

    public static class DbSetUpdateFromSelectList
    {
       
        public static async Task UpdateFromSelectList<JoiningType, CurrentObjType>
            (this DbSet<JoiningType> dbSet, CurrentObjType currentObject, int[] originalSelection, int[] newSelection, Relationship relationship = Relationship.Straight)
            where JoiningType : class, new()
            where CurrentObjType : class
        {
            

            Type joininigType = typeof(JoiningType);
            Type currentObjectsType = typeof(CurrentObjType);

            var objectsIdAsFK = joininigType.GetId(currentObjectsType, IdOptions.Objects);
            var relatedIdAsFK = joininigType.GetId(currentObjectsType, IdOptions.Related);

            var objectsOwnId = currentObjectsType.GetId(currentObjectsType, IdOptions.Own);

            int currentId = (int)objectsOwnId.GetValue(currentObject);

            if (relationship == Relationship.Reversed)
            {
                var temp = objectsIdAsFK;
                objectsIdAsFK = relatedIdAsFK;
                relatedIdAsFK = temp;
            }

            var removedRelations = originalSelection
                .Where(x => !newSelection.Contains(x))
                .Select(async x => await dbSet.FirstOrDefaultAsync
                        (data =>
                        ((int)objectsIdAsFK.GetValue(data)).Equals(currentId)
                        && ((int)relatedIdAsFK.GetValue(data)).Equals(x)));

            
            
            foreach (var item in removedRelations)
            {
                if(await item != null) dbSet.Remove(await item);
            }
            
            foreach(int selectedId in newSelection)
            {
                if(!originalSelection.Contains(selectedId))
                {
                    JoiningType newRelation = new JoiningType();
                    objectsIdAsFK.SetValue(newRelation, currentId);
                    relatedIdAsFK.SetValue(newRelation, selectedId);

                   await dbSet.AddAsync(newRelation);
                }
            }
        }
    }
}
