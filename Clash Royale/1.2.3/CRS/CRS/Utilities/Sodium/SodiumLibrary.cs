using System;
using System.Runtime.InteropServices;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000048 RID: 72
	public static class SodiumLibrary
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000FA94 File Offset: 0x0000DC94
		internal static bool IsRunningOnMono
		{
			get
			{
				return Type.GetType("Mono.Runtime") != null;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000FAA6 File Offset: 0x0000DCA6
		internal static bool Is64
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		internal static string Name
		{
			get
			{
				string text = (SodiumLibrary.Is64 ? "libsodium-64.dll" : "libsodium.dll");
				if (SodiumLibrary.IsRunningOnMono)
				{
					text = "libsodium";
				}
				return text;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000FADF File Offset: 0x0000DCDF
		internal static SodiumLibrary._Init init
		{
			get
			{
				return SodiumLibrary._init.Method;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		internal static SodiumLibrary._GetRandomBytes randombytes_buff
		{
			get
			{
				return SodiumLibrary._randombytes_buff.Method;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000FAF7 File Offset: 0x0000DCF7
		internal static SodiumLibrary._GetRandomNumber randombytes_uniform
		{
			get
			{
				return SodiumLibrary._randombytes_uniform.Method;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000FB03 File Offset: 0x0000DD03
		internal static SodiumLibrary._SodiumIncrement sodium_increment
		{
			get
			{
				return SodiumLibrary._sodium_increment.Method;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000FB0F File Offset: 0x0000DD0F
		internal static SodiumLibrary._SodiumCompare sodium_compare
		{
			get
			{
				return SodiumLibrary._sodium_compare.Method;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000FB1B File Offset: 0x0000DD1B
		internal static SodiumLibrary._SodiumVersionString sodium_version_string
		{
			get
			{
				return SodiumLibrary._sodium_version_string.Method;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000FB27 File Offset: 0x0000DD27
		internal static SodiumLibrary._CryptoHash crypto_hash
		{
			get
			{
				return SodiumLibrary._crypto_hash.Method;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000FB33 File Offset: 0x0000DD33
		internal static SodiumLibrary._Sha512 crypto_hash_sha512
		{
			get
			{
				return SodiumLibrary._crypto_hash_sha512.Method;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000FB3F File Offset: 0x0000DD3F
		internal static SodiumLibrary._Sha256 crypto_hash_sha256
		{
			get
			{
				return SodiumLibrary._crypto_hash_sha256.Method;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000FB4B File Offset: 0x0000DD4B
		internal static SodiumLibrary._GenericHash crypto_generichash
		{
			get
			{
				return SodiumLibrary._crypto_generichash.Method;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000FB57 File Offset: 0x0000DD57
		internal static SodiumLibrary._GenericHashSaltPersonal crypto_generichash_blake2b_salt_personal
		{
			get
			{
				return SodiumLibrary._crypto_generichash_blake2b_salt_personal.Method;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000FB63 File Offset: 0x0000DD63
		internal static SodiumLibrary._OneTimeSign crypto_onetimeauth
		{
			get
			{
				return SodiumLibrary._crypto_onetimeauth.Method;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000FB6F File Offset: 0x0000DD6F
		internal static SodiumLibrary._OneTimeVerify crypto_onetimeauth_verify
		{
			get
			{
				return SodiumLibrary._crypto_onetimeauth_verify.Method;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000FB7B File Offset: 0x0000DD7B
		internal static SodiumLibrary._ArgonHashString crypto_pwhash_str
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_str.Method;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000FB87 File Offset: 0x0000DD87
		internal static SodiumLibrary._ArgonHashVerify crypto_pwhash_str_verify
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_str_verify.Method;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000FB93 File Offset: 0x0000DD93
		internal static SodiumLibrary._ArgonHashBinary crypto_pwhash
		{
			get
			{
				return SodiumLibrary._crypto_pwhash.Method;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000FB9F File Offset: 0x0000DD9F
		internal static SodiumLibrary._HashString crypto_pwhash_scryptsalsa208sha256_str
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_scryptsalsa208sha256_str.Method;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000FBAB File Offset: 0x0000DDAB
		internal static SodiumLibrary._HashBinary crypto_pwhash_scryptsalsa208sha256
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_scryptsalsa208sha256.Method;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000FBB7 File Offset: 0x0000DDB7
		internal static SodiumLibrary._HashVerify crypto_pwhash_scryptsalsa208sha256_str_verify
		{
			get
			{
				return SodiumLibrary._crypto_pwhash_scryptsalsa208sha256_str_verify.Method;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000FBC3 File Offset: 0x0000DDC3
		internal static SodiumLibrary._GenerateKeyPair crypto_sign_keypair
		{
			get
			{
				return SodiumLibrary._crypto_sign_keypair.Method;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000FBCF File Offset: 0x0000DDCF
		internal static SodiumLibrary._GenerateKeyPairFromSeed crypto_sign_seed_keypair
		{
			get
			{
				return SodiumLibrary._crypto_sign_seed_keypair.Method;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000FBDB File Offset: 0x0000DDDB
		internal static SodiumLibrary._Sign crypto_sign
		{
			get
			{
				return SodiumLibrary._crypto_sign.Method;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		internal static SodiumLibrary._Verify crypto_sign_open
		{
			get
			{
				return SodiumLibrary._crypto_sign_open.Method;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000FBF3 File Offset: 0x0000DDF3
		internal static SodiumLibrary._SignDetached crypto_sign_detached
		{
			get
			{
				return SodiumLibrary._crypto_sign_detached.Method;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000FBFF File Offset: 0x0000DDFF
		internal static SodiumLibrary._VerifyDetached crypto_sign_verify_detached
		{
			get
			{
				return SodiumLibrary._crypto_sign_verify_detached.Method;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000FC0B File Offset: 0x0000DE0B
		internal static SodiumLibrary._Ed25519SecretKeyToEd25519Seed crypto_sign_ed25519_sk_to_seed
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_sk_to_seed.Method;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000FC17 File Offset: 0x0000DE17
		internal static SodiumLibrary._Ed25519SecretKeyToEd25519PublicKey crypto_sign_ed25519_sk_to_pk
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_sk_to_pk.Method;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000FC23 File Offset: 0x0000DE23
		internal static SodiumLibrary._Ed25519PublicKeyToCurve25519PublicKey crypto_sign_ed25519_pk_to_curve25519
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_pk_to_curve25519.Method;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000FC2F File Offset: 0x0000DE2F
		internal static SodiumLibrary._Ed25519SecretKeyToCurve25519SecretKey crypto_sign_ed25519_sk_to_curve25519
		{
			get
			{
				return SodiumLibrary._crypto_sign_ed25519_sk_to_curve25519.Method;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000FC3B File Offset: 0x0000DE3B
		internal static SodiumLibrary._GenerateBoxKeyPair crypto_box_keypair
		{
			get
			{
				return SodiumLibrary._crypto_box_keypair.Method;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000FC47 File Offset: 0x0000DE47
		internal static SodiumLibrary._Create crypto_box_easy
		{
			get
			{
				return SodiumLibrary._crypto_box_easy.Method;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000FC53 File Offset: 0x0000DE53
		internal static SodiumLibrary._CreateDetached crypto_box_detached
		{
			get
			{
				return SodiumLibrary._crypto_box_detached.Method;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000FC5F File Offset: 0x0000DE5F
		internal static SodiumLibrary._Open crypto_box_open_easy
		{
			get
			{
				return SodiumLibrary._crypto_box_open_easy.Method;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000FC6B File Offset: 0x0000DE6B
		internal static SodiumLibrary._OpenDetached crypto_box_open_detached
		{
			get
			{
				return SodiumLibrary._crypto_box_open_detached.Method;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000FC77 File Offset: 0x0000DE77
		internal static SodiumLibrary._Bytes crypto_scalarmult_bytes
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_bytes.Method;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000FC83 File Offset: 0x0000DE83
		internal static SodiumLibrary._ScalarBytes crypto_scalarmult_scalarbytes
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_scalarbytes.Method;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000FC8F File Offset: 0x0000DE8F
		internal static SodiumLibrary._Primitive crypto_scalarmult_primitive
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_primitive.Method;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000FC9B File Offset: 0x0000DE9B
		internal static SodiumLibrary._Base crypto_scalarmult_base
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult_base.Method;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000FCA7 File Offset: 0x0000DEA7
		internal static SodiumLibrary._ScalarMult crypto_scalarmult
		{
			get
			{
				return SodiumLibrary._crypto_scalarmult.Method;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		internal static SodiumLibrary._CreateSeal crypto_box_seal
		{
			get
			{
				return SodiumLibrary._crypto_box_seal.Method;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000FCBF File Offset: 0x0000DEBF
		internal static SodiumLibrary._OpenSeal crypto_box_seal_open
		{
			get
			{
				return SodiumLibrary._crypto_box_seal_open.Method;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000FCCB File Offset: 0x0000DECB
		internal static SodiumLibrary._CreateSecret crypto_secretbox
		{
			get
			{
				return SodiumLibrary._crypto_secretbox.Method;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000FCD7 File Offset: 0x0000DED7
		internal static SodiumLibrary._OpenSecret crypto_secretbox_open
		{
			get
			{
				return SodiumLibrary._crypto_secretbox_open.Method;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000FCE3 File Offset: 0x0000DEE3
		internal static SodiumLibrary._CreateSecretDetached crypto_secretbox_detached
		{
			get
			{
				return SodiumLibrary._crypto_secretbox_detached.Method;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000FCEF File Offset: 0x0000DEEF
		internal static SodiumLibrary._OpenSecretDetached crypto_secretbox_open_detached
		{
			get
			{
				return SodiumLibrary._crypto_secretbox_open_detached.Method;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000FCFB File Offset: 0x0000DEFB
		internal static SodiumLibrary._Auth crypto_auth
		{
			get
			{
				return SodiumLibrary._crypto_auth.Method;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000FD07 File Offset: 0x0000DF07
		internal static SodiumLibrary._VerifyAuth crypto_auth_verify
		{
			get
			{
				return SodiumLibrary._crypto_auth_verify.Method;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000FD13 File Offset: 0x0000DF13
		internal static SodiumLibrary._HmacSha256 crypto_auth_hmacsha256
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha256.Method;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000FD1F File Offset: 0x0000DF1F
		internal static SodiumLibrary._HmacSha256Verify crypto_auth_hmacsha256_verify
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha256_verify.Method;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000FD2B File Offset: 0x0000DF2B
		internal static SodiumLibrary._HmacSha512 crypto_auth_hmacsha512
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha512.Method;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000FD37 File Offset: 0x0000DF37
		internal static SodiumLibrary._HmacSha512Verify crypto_auth_hmacsha512_verify
		{
			get
			{
				return SodiumLibrary._crypto_auth_hmacsha512_verify.Method;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000FD43 File Offset: 0x0000DF43
		internal static SodiumLibrary._ShortHash crypto_shorthash
		{
			get
			{
				return SodiumLibrary._crypto_shorthash.Method;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000FD4F File Offset: 0x0000DF4F
		internal static SodiumLibrary._Encrypt crypto_stream_xor
		{
			get
			{
				return SodiumLibrary._crypto_stream_xor.Method;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000FD5B File Offset: 0x0000DF5B
		internal static SodiumLibrary._EncryptChaCha20 crypto_stream_chacha20_xor
		{
			get
			{
				return SodiumLibrary._crypto_stream_chacha20_xor.Method;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000FD67 File Offset: 0x0000DF67
		internal static SodiumLibrary._Bin2Hex sodium_bin2hex
		{
			get
			{
				return SodiumLibrary._sodium_bin2hex.Method;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000FD73 File Offset: 0x0000DF73
		internal static SodiumLibrary._Hex2Bin sodium_hex2bin
		{
			get
			{
				return SodiumLibrary._sodium_hex2bin.Method;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000FD7F File Offset: 0x0000DF7F
		internal static SodiumLibrary._EncryptAead crypto_aead_chacha20poly1305_encrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_chacha20poly1305_encrypt.Method;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000FD8B File Offset: 0x0000DF8B
		internal static SodiumLibrary._DecryptAead crypto_aead_chacha20poly1305_decrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_chacha20poly1305_decrypt.Method;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000FD97 File Offset: 0x0000DF97
		internal static SodiumLibrary._AesAvailable crypto_aead_aes256gcm_is_available
		{
			get
			{
				return SodiumLibrary._crypto_aead_aes256gcm_is_available.Method;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000FDA3 File Offset: 0x0000DFA3
		internal static SodiumLibrary._AesEncrypt crypto_aead_aes256gcm_encrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_aes256gcm_encrypt.Method;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000FDAF File Offset: 0x0000DFAF
		internal static SodiumLibrary._DecryptAes crypto_aead_aes256gcm_decrypt
		{
			get
			{
				return SodiumLibrary._crypto_aead_aes256gcm_decrypt.Method;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000FDBB File Offset: 0x0000DFBB
		internal static SodiumLibrary._HashInit hash_init
		{
			get
			{
				return SodiumLibrary._hash_init.Method;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000FDC7 File Offset: 0x0000DFC7
		internal static SodiumLibrary._HashUpdate hash_update
		{
			get
			{
				return SodiumLibrary._hash_update.Method;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000FDD3 File Offset: 0x0000DFD3
		internal static SodiumLibrary._HashFinal hash_final
		{
			get
			{
				return SodiumLibrary._hash_final.Method;
			}
		}

		// Token: 0x040001C6 RID: 454
		internal static LazyInvoke<SodiumLibrary._Init> _init = new LazyInvoke<SodiumLibrary._Init>("sodium_init", SodiumLibrary.Name);

		// Token: 0x040001C7 RID: 455
		internal static LazyInvoke<SodiumLibrary._GetRandomBytes> _randombytes_buff = new LazyInvoke<SodiumLibrary._GetRandomBytes>("randombytes_buf", SodiumLibrary.Name);

		// Token: 0x040001C8 RID: 456
		internal static LazyInvoke<SodiumLibrary._GetRandomNumber> _randombytes_uniform = new LazyInvoke<SodiumLibrary._GetRandomNumber>("randombytes_uniform", SodiumLibrary.Name);

		// Token: 0x040001C9 RID: 457
		internal static LazyInvoke<SodiumLibrary._SodiumIncrement> _sodium_increment = new LazyInvoke<SodiumLibrary._SodiumIncrement>("sodium_increment", SodiumLibrary.Name);

		// Token: 0x040001CA RID: 458
		internal static LazyInvoke<SodiumLibrary._SodiumCompare> _sodium_compare = new LazyInvoke<SodiumLibrary._SodiumCompare>("sodium_compare", SodiumLibrary.Name);

		// Token: 0x040001CB RID: 459
		internal static LazyInvoke<SodiumLibrary._SodiumVersionString> _sodium_version_string = new LazyInvoke<SodiumLibrary._SodiumVersionString>("sodium_version_string", SodiumLibrary.Name);

		// Token: 0x040001CC RID: 460
		internal static LazyInvoke<SodiumLibrary._CryptoHash> _crypto_hash = new LazyInvoke<SodiumLibrary._CryptoHash>("crypto_hash", SodiumLibrary.Name);

		// Token: 0x040001CD RID: 461
		internal static LazyInvoke<SodiumLibrary._Sha512> _crypto_hash_sha512 = new LazyInvoke<SodiumLibrary._Sha512>("crypto_hash_sha512", SodiumLibrary.Name);

		// Token: 0x040001CE RID: 462
		internal static LazyInvoke<SodiumLibrary._Sha256> _crypto_hash_sha256 = new LazyInvoke<SodiumLibrary._Sha256>("crypto_hash_sha256", SodiumLibrary.Name);

		// Token: 0x040001CF RID: 463
		internal static LazyInvoke<SodiumLibrary._GenericHash> _crypto_generichash = new LazyInvoke<SodiumLibrary._GenericHash>("crypto_generichash", SodiumLibrary.Name);

		// Token: 0x040001D0 RID: 464
		internal static LazyInvoke<SodiumLibrary._GenericHashSaltPersonal> _crypto_generichash_blake2b_salt_personal = new LazyInvoke<SodiumLibrary._GenericHashSaltPersonal>("crypto_generichash_blake2b_salt_personal", SodiumLibrary.Name);

		// Token: 0x040001D1 RID: 465
		internal static LazyInvoke<SodiumLibrary._OneTimeSign> _crypto_onetimeauth = new LazyInvoke<SodiumLibrary._OneTimeSign>("crypto_onetimeauth", SodiumLibrary.Name);

		// Token: 0x040001D2 RID: 466
		internal static LazyInvoke<SodiumLibrary._OneTimeVerify> _crypto_onetimeauth_verify = new LazyInvoke<SodiumLibrary._OneTimeVerify>("crypto_onetimeauth_verify", SodiumLibrary.Name);

		// Token: 0x040001D3 RID: 467
		internal static LazyInvoke<SodiumLibrary._ArgonHashString> _crypto_pwhash_str = new LazyInvoke<SodiumLibrary._ArgonHashString>("crypto_pwhash_argon2i_str", SodiumLibrary.Name);

		// Token: 0x040001D4 RID: 468
		internal static LazyInvoke<SodiumLibrary._ArgonHashVerify> _crypto_pwhash_str_verify = new LazyInvoke<SodiumLibrary._ArgonHashVerify>("crypto_pwhash_argon2i_str_verify", SodiumLibrary.Name);

		// Token: 0x040001D5 RID: 469
		internal static LazyInvoke<SodiumLibrary._ArgonHashBinary> _crypto_pwhash = new LazyInvoke<SodiumLibrary._ArgonHashBinary>("crypto_pwhash_argon2i", SodiumLibrary.Name);

		// Token: 0x040001D6 RID: 470
		internal static LazyInvoke<SodiumLibrary._HashString> _crypto_pwhash_scryptsalsa208sha256_str = new LazyInvoke<SodiumLibrary._HashString>("crypto_pwhash_scryptsalsa208sha256_str", SodiumLibrary.Name);

		// Token: 0x040001D7 RID: 471
		internal static LazyInvoke<SodiumLibrary._HashBinary> _crypto_pwhash_scryptsalsa208sha256 = new LazyInvoke<SodiumLibrary._HashBinary>("crypto_pwhash_scryptsalsa208sha256", SodiumLibrary.Name);

		// Token: 0x040001D8 RID: 472
		internal static LazyInvoke<SodiumLibrary._HashVerify> _crypto_pwhash_scryptsalsa208sha256_str_verify = new LazyInvoke<SodiumLibrary._HashVerify>("crypto_pwhash_scryptsalsa208sha256_str_verify", SodiumLibrary.Name);

		// Token: 0x040001D9 RID: 473
		internal static LazyInvoke<SodiumLibrary._GenerateKeyPair> _crypto_sign_keypair = new LazyInvoke<SodiumLibrary._GenerateKeyPair>("crypto_sign_keypair", SodiumLibrary.Name);

		// Token: 0x040001DA RID: 474
		internal static LazyInvoke<SodiumLibrary._GenerateKeyPairFromSeed> _crypto_sign_seed_keypair = new LazyInvoke<SodiumLibrary._GenerateKeyPairFromSeed>("crypto_sign_seed_keypair", SodiumLibrary.Name);

		// Token: 0x040001DB RID: 475
		internal static LazyInvoke<SodiumLibrary._Sign> _crypto_sign = new LazyInvoke<SodiumLibrary._Sign>("crypto_sign", SodiumLibrary.Name);

		// Token: 0x040001DC RID: 476
		internal static LazyInvoke<SodiumLibrary._Verify> _crypto_sign_open = new LazyInvoke<SodiumLibrary._Verify>("crypto_sign_open", SodiumLibrary.Name);

		// Token: 0x040001DD RID: 477
		internal static LazyInvoke<SodiumLibrary._SignDetached> _crypto_sign_detached = new LazyInvoke<SodiumLibrary._SignDetached>("crypto_sign_detached", SodiumLibrary.Name);

		// Token: 0x040001DE RID: 478
		internal static LazyInvoke<SodiumLibrary._VerifyDetached> _crypto_sign_verify_detached = new LazyInvoke<SodiumLibrary._VerifyDetached>("crypto_sign_verify_detached", SodiumLibrary.Name);

		// Token: 0x040001DF RID: 479
		internal static LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519Seed> _crypto_sign_ed25519_sk_to_seed = new LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519Seed>("crypto_sign_ed25519_sk_to_seed", SodiumLibrary.Name);

		// Token: 0x040001E0 RID: 480
		internal static LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519PublicKey> _crypto_sign_ed25519_sk_to_pk = new LazyInvoke<SodiumLibrary._Ed25519SecretKeyToEd25519PublicKey>("crypto_sign_ed25519_sk_to_pk", SodiumLibrary.Name);

		// Token: 0x040001E1 RID: 481
		internal static LazyInvoke<SodiumLibrary._Ed25519PublicKeyToCurve25519PublicKey> _crypto_sign_ed25519_pk_to_curve25519 = new LazyInvoke<SodiumLibrary._Ed25519PublicKeyToCurve25519PublicKey>("crypto_sign_ed25519_pk_to_curve25519", SodiumLibrary.Name);

		// Token: 0x040001E2 RID: 482
		internal static LazyInvoke<SodiumLibrary._Ed25519SecretKeyToCurve25519SecretKey> _crypto_sign_ed25519_sk_to_curve25519 = new LazyInvoke<SodiumLibrary._Ed25519SecretKeyToCurve25519SecretKey>("crypto_sign_ed25519_sk_to_curve25519", SodiumLibrary.Name);

		// Token: 0x040001E3 RID: 483
		internal static LazyInvoke<SodiumLibrary._GenerateBoxKeyPair> _crypto_box_keypair = new LazyInvoke<SodiumLibrary._GenerateBoxKeyPair>("crypto_box_keypair", SodiumLibrary.Name);

		// Token: 0x040001E4 RID: 484
		internal static LazyInvoke<SodiumLibrary._Create> _crypto_box_easy = new LazyInvoke<SodiumLibrary._Create>("crypto_box_easy", SodiumLibrary.Name);

		// Token: 0x040001E5 RID: 485
		internal static LazyInvoke<SodiumLibrary._CreateDetached> _crypto_box_detached = new LazyInvoke<SodiumLibrary._CreateDetached>("crypto_box_detached", SodiumLibrary.Name);

		// Token: 0x040001E6 RID: 486
		internal static LazyInvoke<SodiumLibrary._Open> _crypto_box_open_easy = new LazyInvoke<SodiumLibrary._Open>("crypto_box_open_easy", SodiumLibrary.Name);

		// Token: 0x040001E7 RID: 487
		internal static LazyInvoke<SodiumLibrary._OpenDetached> _crypto_box_open_detached = new LazyInvoke<SodiumLibrary._OpenDetached>("crypto_box_open_detached", SodiumLibrary.Name);

		// Token: 0x040001E8 RID: 488
		internal static LazyInvoke<SodiumLibrary._Bytes> _crypto_scalarmult_bytes = new LazyInvoke<SodiumLibrary._Bytes>("crypto_scalarmult_bytes", SodiumLibrary.Name);

		// Token: 0x040001E9 RID: 489
		internal static LazyInvoke<SodiumLibrary._ScalarBytes> _crypto_scalarmult_scalarbytes = new LazyInvoke<SodiumLibrary._ScalarBytes>("crypto_scalarmult_scalarbytes", SodiumLibrary.Name);

		// Token: 0x040001EA RID: 490
		internal static LazyInvoke<SodiumLibrary._Primitive> _crypto_scalarmult_primitive = new LazyInvoke<SodiumLibrary._Primitive>("crypto_scalarmult_primitive", SodiumLibrary.Name);

		// Token: 0x040001EB RID: 491
		internal static LazyInvoke<SodiumLibrary._Base> _crypto_scalarmult_base = new LazyInvoke<SodiumLibrary._Base>("crypto_scalarmult_base", SodiumLibrary.Name);

		// Token: 0x040001EC RID: 492
		internal static LazyInvoke<SodiumLibrary._ScalarMult> _crypto_scalarmult = new LazyInvoke<SodiumLibrary._ScalarMult>("crypto_scalarmult", SodiumLibrary.Name);

		// Token: 0x040001ED RID: 493
		internal static LazyInvoke<SodiumLibrary._CreateSeal> _crypto_box_seal = new LazyInvoke<SodiumLibrary._CreateSeal>("crypto_box_seal", SodiumLibrary.Name);

		// Token: 0x040001EE RID: 494
		internal static LazyInvoke<SodiumLibrary._OpenSeal> _crypto_box_seal_open = new LazyInvoke<SodiumLibrary._OpenSeal>("crypto_box_seal_open", SodiumLibrary.Name);

		// Token: 0x040001EF RID: 495
		internal static LazyInvoke<SodiumLibrary._CreateSecret> _crypto_secretbox = new LazyInvoke<SodiumLibrary._CreateSecret>("crypto_secretbox", SodiumLibrary.Name);

		// Token: 0x040001F0 RID: 496
		internal static LazyInvoke<SodiumLibrary._OpenSecret> _crypto_secretbox_open = new LazyInvoke<SodiumLibrary._OpenSecret>("crypto_secretbox_open", SodiumLibrary.Name);

		// Token: 0x040001F1 RID: 497
		internal static LazyInvoke<SodiumLibrary._CreateSecretDetached> _crypto_secretbox_detached = new LazyInvoke<SodiumLibrary._CreateSecretDetached>("crypto_secretbox_detached", SodiumLibrary.Name);

		// Token: 0x040001F2 RID: 498
		internal static LazyInvoke<SodiumLibrary._OpenSecretDetached> _crypto_secretbox_open_detached = new LazyInvoke<SodiumLibrary._OpenSecretDetached>("crypto_secretbox_open_detached", SodiumLibrary.Name);

		// Token: 0x040001F3 RID: 499
		internal static LazyInvoke<SodiumLibrary._Auth> _crypto_auth = new LazyInvoke<SodiumLibrary._Auth>("crypto_auth", SodiumLibrary.Name);

		// Token: 0x040001F4 RID: 500
		internal static LazyInvoke<SodiumLibrary._VerifyAuth> _crypto_auth_verify = new LazyInvoke<SodiumLibrary._VerifyAuth>("crypto_auth_verify", SodiumLibrary.Name);

		// Token: 0x040001F5 RID: 501
		internal static LazyInvoke<SodiumLibrary._HmacSha256> _crypto_auth_hmacsha256 = new LazyInvoke<SodiumLibrary._HmacSha256>("crypto_auth_hmacsha256", SodiumLibrary.Name);

		// Token: 0x040001F6 RID: 502
		internal static LazyInvoke<SodiumLibrary._HmacSha256Verify> _crypto_auth_hmacsha256_verify = new LazyInvoke<SodiumLibrary._HmacSha256Verify>("crypto_auth_hmacsha256_verify", SodiumLibrary.Name);

		// Token: 0x040001F7 RID: 503
		internal static LazyInvoke<SodiumLibrary._HmacSha512> _crypto_auth_hmacsha512 = new LazyInvoke<SodiumLibrary._HmacSha512>("crypto_auth_hmacsha512", SodiumLibrary.Name);

		// Token: 0x040001F8 RID: 504
		internal static LazyInvoke<SodiumLibrary._HmacSha512Verify> _crypto_auth_hmacsha512_verify = new LazyInvoke<SodiumLibrary._HmacSha512Verify>("crypto_auth_hmacsha512_verify", SodiumLibrary.Name);

		// Token: 0x040001F9 RID: 505
		internal static LazyInvoke<SodiumLibrary._ShortHash> _crypto_shorthash = new LazyInvoke<SodiumLibrary._ShortHash>("crypto_shorthash", SodiumLibrary.Name);

		// Token: 0x040001FA RID: 506
		internal static LazyInvoke<SodiumLibrary._Encrypt> _crypto_stream_xor = new LazyInvoke<SodiumLibrary._Encrypt>("crypto_stream_xor", SodiumLibrary.Name);

		// Token: 0x040001FB RID: 507
		internal static LazyInvoke<SodiumLibrary._EncryptChaCha20> _crypto_stream_chacha20_xor = new LazyInvoke<SodiumLibrary._EncryptChaCha20>("crypto_stream_chacha20_xor", SodiumLibrary.Name);

		// Token: 0x040001FC RID: 508
		internal static LazyInvoke<SodiumLibrary._Bin2Hex> _sodium_bin2hex = new LazyInvoke<SodiumLibrary._Bin2Hex>("sodium_bin2hex", SodiumLibrary.Name);

		// Token: 0x040001FD RID: 509
		internal static LazyInvoke<SodiumLibrary._Hex2Bin> _sodium_hex2bin = new LazyInvoke<SodiumLibrary._Hex2Bin>("sodium_hex2bin", SodiumLibrary.Name);

		// Token: 0x040001FE RID: 510
		internal static LazyInvoke<SodiumLibrary._EncryptAead> _crypto_aead_chacha20poly1305_encrypt = new LazyInvoke<SodiumLibrary._EncryptAead>("crypto_aead_chacha20poly1305_encrypt", SodiumLibrary.Name);

		// Token: 0x040001FF RID: 511
		internal static LazyInvoke<SodiumLibrary._DecryptAead> _crypto_aead_chacha20poly1305_decrypt = new LazyInvoke<SodiumLibrary._DecryptAead>("crypto_aead_chacha20poly1305_decrypt", SodiumLibrary.Name);

		// Token: 0x04000200 RID: 512
		internal static LazyInvoke<SodiumLibrary._AesAvailable> _crypto_aead_aes256gcm_is_available = new LazyInvoke<SodiumLibrary._AesAvailable>("crypto_aead_aes256gcm_is_available", SodiumLibrary.Name);

		// Token: 0x04000201 RID: 513
		internal static LazyInvoke<SodiumLibrary._AesEncrypt> _crypto_aead_aes256gcm_encrypt = new LazyInvoke<SodiumLibrary._AesEncrypt>("crypto_aead_aes256gcm_encrypt", SodiumLibrary.Name);

		// Token: 0x04000202 RID: 514
		internal static LazyInvoke<SodiumLibrary._DecryptAes> _crypto_aead_aes256gcm_decrypt = new LazyInvoke<SodiumLibrary._DecryptAes>("crypto_aead_aes256gcm_decrypt", SodiumLibrary.Name);

		// Token: 0x04000203 RID: 515
		internal static LazyInvoke<SodiumLibrary._HashInit> _hash_init = new LazyInvoke<SodiumLibrary._HashInit>("crypto_generichash_init", SodiumLibrary.Name);

		// Token: 0x04000204 RID: 516
		internal static LazyInvoke<SodiumLibrary._HashUpdate> _hash_update = new LazyInvoke<SodiumLibrary._HashUpdate>("crypto_generichash_update", SodiumLibrary.Name);

		// Token: 0x04000205 RID: 517
		internal static LazyInvoke<SodiumLibrary._HashFinal> _hash_final = new LazyInvoke<SodiumLibrary._HashFinal>("crypto_generichash_final", SodiumLibrary.Name);

		// Token: 0x020000DA RID: 218
		// (Invoke) Token: 0x060005F8 RID: 1528
		internal delegate void _Init();

		// Token: 0x020000DB RID: 219
		// (Invoke) Token: 0x060005FC RID: 1532
		internal delegate void _GetRandomBytes(byte[] buffer, int size);

		// Token: 0x020000DC RID: 220
		// (Invoke) Token: 0x06000600 RID: 1536
		internal delegate int _GetRandomNumber(int upperBound);

		// Token: 0x020000DD RID: 221
		// (Invoke) Token: 0x06000604 RID: 1540
		internal delegate void _SodiumIncrement(byte[] buffer, long length);

		// Token: 0x020000DE RID: 222
		// (Invoke) Token: 0x06000608 RID: 1544
		internal delegate int _SodiumCompare(byte[] a, byte[] b, long length);

		// Token: 0x020000DF RID: 223
		// (Invoke) Token: 0x0600060C RID: 1548
		internal delegate IntPtr _SodiumVersionString();

		// Token: 0x020000E0 RID: 224
		// (Invoke) Token: 0x06000610 RID: 1552
		internal delegate int _CryptoHash(byte[] buffer, byte[] message, long length);

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x06000614 RID: 1556
		internal delegate int _Sha512(byte[] buffer, byte[] message, long length);

		// Token: 0x020000E2 RID: 226
		// (Invoke) Token: 0x06000618 RID: 1560
		internal delegate int _Sha256(byte[] buffer, byte[] message, long length);

		// Token: 0x020000E3 RID: 227
		// (Invoke) Token: 0x0600061C RID: 1564
		internal delegate int _GenericHash(byte[] buffer, int bufferLength, byte[] message, long messageLength, byte[] key, int keyLength);

		// Token: 0x020000E4 RID: 228
		// (Invoke) Token: 0x06000620 RID: 1568
		internal delegate int _GenericHashSaltPersonal(byte[] buffer, int bufferLength, byte[] message, long messageLength, byte[] key, int keyLength, byte[] salt, byte[] personal);

		// Token: 0x020000E5 RID: 229
		// (Invoke) Token: 0x06000624 RID: 1572
		internal delegate int _OneTimeSign(byte[] buffer, byte[] message, long messageLength, byte[] key);

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x06000628 RID: 1576
		internal delegate int _OneTimeVerify(byte[] signature, byte[] message, long messageLength, byte[] key);

		// Token: 0x020000E7 RID: 231
		// (Invoke) Token: 0x0600062C RID: 1580
		internal delegate int _ArgonHashString(byte[] buffer, byte[] password, long passwordLen, long opsLimit, int memLimit);

		// Token: 0x020000E8 RID: 232
		// (Invoke) Token: 0x06000630 RID: 1584
		internal delegate int _ArgonHashVerify(byte[] buffer, byte[] password, long passLength);

		// Token: 0x020000E9 RID: 233
		// (Invoke) Token: 0x06000634 RID: 1588
		internal delegate int _ArgonHashBinary(byte[] buffer, long bufferLen, byte[] password, long passwordLen, byte[] salt, long opsLimit, int memLimit, int alg);

		// Token: 0x020000EA RID: 234
		// (Invoke) Token: 0x06000638 RID: 1592
		internal delegate int _HashString(byte[] buffer, byte[] password, long passwordLen, long opsLimit, int memLimit);

		// Token: 0x020000EB RID: 235
		// (Invoke) Token: 0x0600063C RID: 1596
		internal delegate int _HashBinary(byte[] buffer, long bufferLen, byte[] password, long passwordLen, byte[] salt, long opsLimit, int memLimit);

		// Token: 0x020000EC RID: 236
		// (Invoke) Token: 0x06000640 RID: 1600
		internal delegate int _HashVerify(byte[] buffer, byte[] password, long passLength);

		// Token: 0x020000ED RID: 237
		// (Invoke) Token: 0x06000644 RID: 1604
		internal delegate int _GenerateKeyPair(byte[] publicKey, byte[] secretKey);

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x06000648 RID: 1608
		internal delegate int _GenerateKeyPairFromSeed(byte[] publicKey, byte[] secretKey, byte[] seed);

		// Token: 0x020000EF RID: 239
		// (Invoke) Token: 0x0600064C RID: 1612
		internal delegate int _Sign(byte[] buffer, ref long bufferLength, byte[] message, long messageLength, byte[] key);

		// Token: 0x020000F0 RID: 240
		// (Invoke) Token: 0x06000650 RID: 1616
		internal delegate int _Verify(byte[] buffer, ref long bufferLength, byte[] signedMessage, long signedMessageLength, byte[] key);

		// Token: 0x020000F1 RID: 241
		// (Invoke) Token: 0x06000654 RID: 1620
		internal delegate int _SignDetached(byte[] signature, ref long signatureLength, byte[] message, long messageLength, byte[] key);

		// Token: 0x020000F2 RID: 242
		// (Invoke) Token: 0x06000658 RID: 1624
		internal delegate int _VerifyDetached(byte[] signature, byte[] message, long messageLength, byte[] key);

		// Token: 0x020000F3 RID: 243
		// (Invoke) Token: 0x0600065C RID: 1628
		internal delegate int _Ed25519SecretKeyToEd25519Seed(byte[] seed, byte[] secretKey);

		// Token: 0x020000F4 RID: 244
		// (Invoke) Token: 0x06000660 RID: 1632
		internal delegate int _Ed25519SecretKeyToEd25519PublicKey(byte[] publicKey, byte[] secretKey);

		// Token: 0x020000F5 RID: 245
		// (Invoke) Token: 0x06000664 RID: 1636
		internal delegate int _Ed25519PublicKeyToCurve25519PublicKey(byte[] curve25519Pk, byte[] ed25519Pk);

		// Token: 0x020000F6 RID: 246
		// (Invoke) Token: 0x06000668 RID: 1640
		internal delegate int _Ed25519SecretKeyToCurve25519SecretKey(byte[] curve25519Sk, byte[] ed25519Sk);

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x0600066C RID: 1644
		internal delegate int _GenerateBoxKeyPair(byte[] publicKey, byte[] secretKey);

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x06000670 RID: 1648
		internal delegate int _Create(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] publicKey, byte[] secretKey);

		// Token: 0x020000F9 RID: 249
		// (Invoke) Token: 0x06000674 RID: 1652
		internal delegate int _Open(byte[] buffer, byte[] cipherText, long cipherTextLength, byte[] nonce, byte[] publicKey, byte[] secretKey);

		// Token: 0x020000FA RID: 250
		// (Invoke) Token: 0x06000678 RID: 1656
		internal delegate int _CreateDetached(byte[] cipher, byte[] mac, byte[] message, long messageLength, byte[] nonce, byte[] pk, byte[] sk);

		// Token: 0x020000FB RID: 251
		// (Invoke) Token: 0x0600067C RID: 1660
		internal delegate int _OpenDetached(byte[] buffer, byte[] cipherText, byte[] mac, long cipherTextLength, byte[] nonce, byte[] pk, byte[] sk);

		// Token: 0x020000FC RID: 252
		// (Invoke) Token: 0x06000680 RID: 1664
		internal delegate int _Bytes();

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x06000684 RID: 1668
		internal delegate int _ScalarBytes();

		// Token: 0x020000FE RID: 254
		// (Invoke) Token: 0x06000688 RID: 1672
		internal delegate byte _Primitive();

		// Token: 0x020000FF RID: 255
		// (Invoke) Token: 0x0600068C RID: 1676
		internal delegate int _Base(byte[] q, byte[] n);

		// Token: 0x02000100 RID: 256
		// (Invoke) Token: 0x06000690 RID: 1680
		internal delegate int _ScalarMult(byte[] q, byte[] n, byte[] p);

		// Token: 0x02000101 RID: 257
		// (Invoke) Token: 0x06000694 RID: 1684
		internal delegate int _CreateSeal(byte[] buffer, byte[] message, long messageLength, byte[] pk);

		// Token: 0x02000102 RID: 258
		// (Invoke) Token: 0x06000698 RID: 1688
		internal delegate int _OpenSeal(byte[] buffer, byte[] cipherText, long cipherTextLength, byte[] pk, byte[] sk);

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x0600069C RID: 1692
		internal delegate int _CreateSecret(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] key);

		// Token: 0x02000104 RID: 260
		// (Invoke) Token: 0x060006A0 RID: 1696
		internal delegate int _OpenSecret(byte[] buffer, byte[] cipherText, long cipherTextLength, byte[] nonce, byte[] key);

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x060006A4 RID: 1700
		internal delegate int _CreateSecretDetached(byte[] cipher, byte[] mac, byte[] message, long messageLength, byte[] nonce, byte[] key);

		// Token: 0x02000106 RID: 262
		// (Invoke) Token: 0x060006A8 RID: 1704
		internal delegate int _OpenSecretDetached(byte[] buffer, byte[] cipherText, byte[] mac, long cipherTextLength, byte[] nonce, byte[] key);

		// Token: 0x02000107 RID: 263
		// (Invoke) Token: 0x060006AC RID: 1708
		internal delegate int _Auth(byte[] buffer, byte[] message, long messageLength, byte[] key);

		// Token: 0x02000108 RID: 264
		// (Invoke) Token: 0x060006B0 RID: 1712
		internal delegate int _VerifyAuth(byte[] signature, byte[] message, long messageLength, byte[] key);

		// Token: 0x02000109 RID: 265
		// (Invoke) Token: 0x060006B4 RID: 1716
		internal delegate int _HmacSha256(byte[] buffer, byte[] message, long messageLength, byte[] key);

		// Token: 0x0200010A RID: 266
		// (Invoke) Token: 0x060006B8 RID: 1720
		internal delegate int _HmacSha256Verify(byte[] signature, byte[] message, long messageLength, byte[] key);

		// Token: 0x0200010B RID: 267
		// (Invoke) Token: 0x060006BC RID: 1724
		internal delegate int _HmacSha512(byte[] signature, byte[] message, long messageLength, byte[] key);

		// Token: 0x0200010C RID: 268
		// (Invoke) Token: 0x060006C0 RID: 1728
		internal delegate int _HmacSha512Verify(byte[] signature, byte[] message, long messageLength, byte[] key);

		// Token: 0x0200010D RID: 269
		// (Invoke) Token: 0x060006C4 RID: 1732
		internal delegate int _ShortHash(byte[] buffer, byte[] message, long messageLength, byte[] key);

		// Token: 0x0200010E RID: 270
		// (Invoke) Token: 0x060006C8 RID: 1736
		internal delegate int _Encrypt(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] key);

		// Token: 0x0200010F RID: 271
		// (Invoke) Token: 0x060006CC RID: 1740
		internal delegate int _EncryptChaCha20(byte[] buffer, byte[] message, long messageLength, byte[] nonce, byte[] key);

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x060006D0 RID: 1744
		internal delegate IntPtr _Bin2Hex(byte[] hex, int hexMaxlen, byte[] bin, int binLen);

		// Token: 0x02000111 RID: 273
		// (Invoke) Token: 0x060006D4 RID: 1748
		internal delegate int _Hex2Bin(IntPtr bin, int binMaxlen, string hex, int hexLen, string ignore, out int binLen, string hexEnd);

		// Token: 0x02000112 RID: 274
		// (Invoke) Token: 0x060006D8 RID: 1752
		internal delegate int _EncryptAead(IntPtr cipher, out long cipherLength, byte[] message, long messageLength, byte[] additionalData, long additionalDataLength, byte[] nsec, byte[] nonce, byte[] key);

		// Token: 0x02000113 RID: 275
		// (Invoke) Token: 0x060006DC RID: 1756
		internal delegate int _DecryptAead(IntPtr message, out long messageLength, byte[] nsec, byte[] cipher, long cipherLength, byte[] additionalData, long additionalDataLength, byte[] nonce, byte[] key);

		// Token: 0x02000114 RID: 276
		// (Invoke) Token: 0x060006E0 RID: 1760
		internal delegate int _AesAvailable();

		// Token: 0x02000115 RID: 277
		// (Invoke) Token: 0x060006E4 RID: 1764
		internal delegate int _AesEncrypt(IntPtr cipher, out long cipherLength, byte[] message, long messageLength, byte[] additionalData, long additionalDataLength, byte[] nsec, byte[] nonce, byte[] key);

		// Token: 0x02000116 RID: 278
		// (Invoke) Token: 0x060006E8 RID: 1768
		internal delegate int _DecryptAes(IntPtr message, out long messageLength, byte[] nsec, byte[] cipher, long cipherLength, byte[] additionalData, long additionalDataLength, byte[] nonce, byte[] key);

		// Token: 0x02000117 RID: 279
		[StructLayout(LayoutKind.Sequential, Size = 384)]
		internal struct _HashState
		{
			// Token: 0x040003B3 RID: 947
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public ulong[] h;

			// Token: 0x040003B4 RID: 948
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public ulong[] t;

			// Token: 0x040003B5 RID: 949
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public ulong[] f;

			// Token: 0x040003B6 RID: 950
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public byte[] buf;

			// Token: 0x040003B7 RID: 951
			public uint buflen;

			// Token: 0x040003B8 RID: 952
			public byte last_node;
		}

		// Token: 0x02000118 RID: 280
		// (Invoke) Token: 0x060006EC RID: 1772
		internal delegate int _HashInit(IntPtr state, byte[] key, int keySize, int hashSize);

		// Token: 0x02000119 RID: 281
		// (Invoke) Token: 0x060006F0 RID: 1776
		internal delegate int _HashUpdate(IntPtr state, byte[] message, long messageLength);

		// Token: 0x0200011A RID: 282
		// (Invoke) Token: 0x060006F4 RID: 1780
		internal delegate int _HashFinal(IntPtr state, byte[] buffer, int bufferLength);
	}
}
