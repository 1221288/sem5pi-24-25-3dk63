using AutoMapper;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.SurgeryRooms;
using DDDSample1.Domain.SurgeryRooms.ValueObjects;
using System;

namespace Backend.Domain.SurgeryRoom
{
    public class SurgeryRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurgeryRoomRepository _surgeryRoomRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomRepository surgeryRoomRepository, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _surgeryRoomRepository = surgeryRoomRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<SurgeryRoomDTO>> GetAllAsync()
        {
            var list = await _surgeryRoomRepository.GetAllAsync();
            return _mapper.Map<List<SurgeryRoomDTO>>(list);
        }

        public async Task<SurgeryRoomDTO> GetByIdAsync(RoomId roomId)
        {
            var surgeryRoomEntity = await _surgeryRoomRepository.GetByIdAsync(roomId);
            return surgeryRoomEntity == null ? null : _mapper.Map<SurgeryRoomDTO>(surgeryRoomEntity);
        }

        public async Task<SurgeryRoomDTO> AddAsync(CreatingSurgeryRoomDto creatingDto)
        {

            if (creatingDto == null)
            {
                throw new ArgumentNullException(nameof(creatingDto), "Creating DTO cannot be null.");
            }

            string domain = _configuration["DNS_DOMAIN"];
            if (string.IsNullOrEmpty(domain))
            {
                throw new BusinessRuleValidationException("O domínio DNS não está configurado corretamente.");
            }


            var surgeryRoomEntity = _mapper.Map<SurgeryRoomEntity>(creatingDto);
            await _surgeryRoomRepository.AddAsync(surgeryRoomEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<SurgeryRoomDTO>(surgeryRoomEntity);
        }

        public async Task<SurgeryRoomDTO> UpdateAsync(SurgeryRoomDTO surgeryRoomDto)
        {
            if (surgeryRoomDto == null)
            {
                throw new ArgumentNullException(nameof(surgeryRoomDto), "SurgeryRoomDTO cannot be null.");
            }

            var existingRoom = await _surgeryRoomRepository.GetByIdAsync(new RoomId(surgeryRoomDto.RoomId));
            if (existingRoom == null)
            {
                throw new BusinessRuleValidationException("Surgery room not found.");
            }

            existingRoom.ChangeRoomNumber(surgeryRoomDto.RoomNumber);
            existingRoom.ChangeRoomType(surgeryRoomDto.Type);
            existingRoom.ChangeCapacity(surgeryRoomDto.Capacity);
            existingRoom.ChangeAssignedEquipment(surgeryRoomDto.AssignedEquipment);
            existingRoom.ChangeCurrentStatus(surgeryRoomDto.CurrentStatus);
            existingRoom.ChangeMaintenanceSlots(surgeryRoomDto.MaintenanceSlots);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<SurgeryRoomDTO>(existingRoom);
        }

        public async Task<SurgeryRoomDTO> DeleteAsync(RoomId roomId)
        {
            var existingRoom = await _surgeryRoomRepository.GetByIdAsync(roomId);
            if (existingRoom == null)
            {
                return null;
            }

            if (existingRoom.CurrentStatus.Value != RoomStatusEnum.Available)
            {
                throw new BusinessRuleValidationException("Cannot delete a surgery room that is not available.");
            }

            this. _surgeryRoomRepository.Remove(existingRoom);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<SurgeryRoomDTO>(existingRoom);
        }


    }
}
