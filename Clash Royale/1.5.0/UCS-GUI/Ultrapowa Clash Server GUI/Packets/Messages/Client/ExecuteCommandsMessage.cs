namespace CRS.Packets
{
    #region Usings

    using System;

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Logic;

    #endregion

    internal class ExecuteCommandsMessage : Message
    {
        public const ushort PacketID = 14102;

        public uint Checksum;

        public byte[] NestedCommands;

        public uint NumberOfCommands;

        public uint Subtick;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ExecuteCommandsMessage" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public ExecuteCommandsMessage(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Execute_Commands.
            //this.Decrypt();
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Subtick = (uint)this.Reader.ReadVInt();
                this.Checksum = (uint)this.Reader.ReadVInt();
                this.NumberOfCommands = (uint)this.Reader.ReadVInt();

            if (this.NumberOfCommands > 0u)
            {
                this.NestedCommands = this.Reader.ReadBytes(this.GetLength());
            }

            //if (NumberOfCommands > 0 && NumberOfCommands < 120)
            //{
            //    NestedCommands = Reader.ReadBytes(GetLength() - 12);
            //}
            //else
            //{
            //    NumberOfCommands = 0;
            //}

            Debug.Write("Subtick: " + this.Subtick);
            Debug.Write("Checksum: " + this.Checksum);
            Debug.Write("NumberOfCommands: " + this.NumberOfCommands);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            try
            {
                level.Tick();
                if (this.NumberOfCommands > 0u)
                {
                    using (Reader _Reader = new Reader(this.NestedCommands))
                    {
                        int num = 0;
                        while ((long)num < (long)((ulong)this.NumberOfCommands))
                        {
                            int _ID = _Reader.ReadVInt();
                            Debug.Write("Command ID: " + _ID + " Name: " + Command_Factory.Commands[_ID].Name);
                            if (Command_Factory.Commands.ContainsKey(_ID))
                            {
                                Command _Command = Activator.CreateInstance(Command_Factory.Commands[_ID], _Reader, this.Client, _ID) as Command;
                                if (_Command != null)
                                {
                                    Debug.Write(string.Concat(new object[]
                                    {
                                                "[CRS]    Processing command ",  Command_Factory.Commands[_ID].Name, " (", num, ")" + "ID: " + _ID
                                    }));
                                    _Command.Decode();
                                    _Command.Process();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Debug.Write("Command " + _ID + " has not been handled.");
                                Console.ResetColor();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Debug.Write(Debug.FlattenException(ex));
                Console.ResetColor();
            }
        }
    }
}