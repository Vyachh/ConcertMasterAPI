// Ignore Spelling: Lcz Jwt

namespace ConcertMasterAPI.Helpers
{
    /// <summary>
    /// �������� ���������, ������������ � ����������, ������� ��������� � ������ ������.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// ��������� �� ������, ����������� �� ��, ��� ������ ��� ����������.
        /// </summary>
        public static string ObjectExists = "����� ������ ��� ����������";

        /// <summary>
        /// �������� �������������� ��������� � ������ ������ ��� ��������, ��������� � ��������������.
        /// </summary>
        public static class UserLcz
        {
            /// <summary>
            /// ��������� �� ������, ����������� �� ������������� ��������� �������������� ������������ �� JWT.
            /// </summary>
            public static string UserIdJwtNotFound = "�� ������� �������� ������������� ������������ �� JWT";

            /// <summary>
            /// ��������� �� ������, ����������� �� �������� ����� ��� ������.
            /// </summary>
            public static string LoginOrPasswordIncorrectMessage = "����� ��� ������ �������";
        }

        /// <summary>
        /// �������� �������������� ��������� � ������ ������ ��� ��������, ��������� � ��������.
        /// </summary>
        public static class TicketLcz
        {
            /// <summary>
            /// ��������� �� ������, ����������� �� ���������� ������.
            /// </summary>
            public static string TicketNotFound = "������ ���";

            /// <summary>
            /// ��������� �� ������, ����������� �� ������������� ���������� ������� ��� ���������� ����.
            /// </summary>
            public static string TicketCountNotEnough = "������������ ������� ��� ���� {0}. ��������: {1}";
        }
    }
}
