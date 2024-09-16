using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Buildings;
using api.Models;

namespace api.Mappers
{
    public static class BuildingMappers
    {
        public static BuildingDto ToBuildingDto(this Building buildingModel)
        {
            return new BuildingDto
            {
                Id = buildingModel.Id,
                Address = buildingModel.Address,
                NumberOfFloors = buildingModel.NumberOfFloors,
                DateBuilt = buildingModel.DateBuilt,
                IsHouse = buildingModel.IsHouse,

            };
        }

        public static Building ToBuildingFromCreateDto(this CreateBuildingRequestDto buildingDto)
        {
            return new Building
            {
                Address = buildingDto.Address,
                NumberOfFloors = buildingDto.NumberOfFloors,
                DateBuilt = buildingDto.DateBuilt,
                IsHouse = buildingDto.IsHouse
            };
        }
    }
}