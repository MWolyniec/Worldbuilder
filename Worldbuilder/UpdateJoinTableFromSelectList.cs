using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Worldbuilder
{
    public enum Relationship { Straight, Reversed }

    public static class UpdateJoinTableFromSelectList
    {

        public static void Update<TJoinTableObject, TObjectToUpdate>
            (DbSet<TJoinTableObject> joinTable, TObjectToUpdate objectToUpdate, int[] originalSelection, int[] newSelection, Relationship relationship = Relationship.Straight)
            where TJoinTableObject : class, new()
            where TObjectToUpdate : class
        {


            Type joininigType = typeof(TJoinTableObject);
            Type currentObjectsType = typeof(TObjectToUpdate);

            var objectsIdAsFK = joininigType.GetId(currentObjectsType, IdOptions.Objects);
            var relatedIdAsFK = joininigType.GetId(currentObjectsType, IdOptions.Related);

            var objectsOwnId = currentObjectsType.GetId(currentObjectsType, IdOptions.Own);

            int currentId = (int)objectsOwnId.GetValue(objectToUpdate);

            if (relationship == Relationship.Reversed)
            {
                var temp = objectsIdAsFK;
                objectsIdAsFK = relatedIdAsFK;
                relatedIdAsFK = temp;
            }


            /*
            var removedRelations = originalSelection
                .Where(x => !newSelection.Contains(x))
                .Select(x => joinTable.FirstOrDefault
                        (data =>
                        ((int)objectsIdAsFK.GetValue(data)).Equals(currentId)
                        && ((int)relatedIdAsFK.GetValue(data)).Equals(x)));
*/

            if (originalSelection != null && originalSelection != Array.Empty<int>())
            {
                IEnumerable<int> removedRelationsIndexes;

                if (newSelection != Array.Empty<int>() && newSelection != null)
                {
                    removedRelationsIndexes = originalSelection.Where(x => !newSelection.Contains(x));

                    foreach (int selectedId in newSelection)
                    {
                        if (!originalSelection.Contains(selectedId))
                        {
                            TJoinTableObject newRelation = new TJoinTableObject();
                            objectsIdAsFK.SetValue(newRelation, currentId);
                            relatedIdAsFK.SetValue(newRelation, selectedId);

                            joinTable.Add(newRelation);
                        }
                    }
                }
                else
                {
                    removedRelationsIndexes = originalSelection;
                }
                foreach (var index in removedRelationsIndexes)
                {
                    foreach (var item in joinTable)
                    {
                        if (((int)objectsIdAsFK.GetValue(item)).Equals(currentId) && ((int)relatedIdAsFK.GetValue(item)).Equals(index))
                        {
                            joinTable.Remove(item);
                        }
                    }

                    /*TJoinTableObject itemToRemove = joinTable.FirstOrDefault
                            (data => ((int)objectsIdAsFK.GetValue(data)).Equals(currentId)
                            && ((int)relatedIdAsFK.GetValue(data)).Equals(index));*/


                    //if (itemToRemove != null) joinTable.Remove(itemToRemove);
                }
            }


        }
    }
}
