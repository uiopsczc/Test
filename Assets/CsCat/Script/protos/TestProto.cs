// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: TestProto.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from TestProto.proto</summary>
public static partial class TestProtoReflection {

  #region Descriptor
  /// <summary>File descriptor for TestProto.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static TestProtoReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Cg9UZXN0UHJvdG8ucHJvdG8ifwoJVGVzdFByb3RvEg8KB2FjY291bnQYASAB",
          "KAkSEAoIcGFzc3dvcmQYAiABKAkSIgoEZGljdBgDIAMoCzIULlRlc3RQcm90",
          "by5EaWN0RW50cnkaKwoJRGljdEVudHJ5EgsKA2tleRgBIAEoCRINCgV2YWx1",
          "ZRgCIAEoCToCOAFiBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::TestProto), global::TestProto.Parser, new[]{ "Account", "Password", "Dict" }, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
        }));
  }
  #endregion

}
#region Messages
public sealed partial class TestProto : pb::IMessage<TestProto> {
  private static readonly pb::MessageParser<TestProto> _parser = new pb::MessageParser<TestProto>(() => new TestProto());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<TestProto> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::TestProtoReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public TestProto() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public TestProto(TestProto other) : this() {
    account_ = other.account_;
    password_ = other.password_;
    dict_ = other.dict_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public TestProto Clone() {
    return new TestProto(this);
  }

  /// <summary>Field number for the "account" field.</summary>
  public const int AccountFieldNumber = 1;
  private string account_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Account {
    get { return account_; }
    set {
      account_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "password" field.</summary>
  public const int PasswordFieldNumber = 2;
  private string password_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Password {
    get { return password_; }
    set {
      password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "dict" field.</summary>
  public const int DictFieldNumber = 3;
  private static readonly pbc::MapField<string, string>.Codec _map_dict_codec
      = new pbc::MapField<string, string>.Codec(pb::FieldCodec.ForString(10), pb::FieldCodec.ForString(18), 26);
  private readonly pbc::MapField<string, string> dict_ = new pbc::MapField<string, string>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::MapField<string, string> Dict {
    get { return dict_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as TestProto);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(TestProto other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Account != other.Account) return false;
    if (Password != other.Password) return false;
    if (!Dict.Equals(other.Dict)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Account.Length != 0) hash ^= Account.GetHashCode();
    if (Password.Length != 0) hash ^= Password.GetHashCode();
    hash ^= Dict.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Account.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Account);
    }
    if (Password.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Password);
    }
    dict_.WriteTo(output, _map_dict_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Account.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Account);
    }
    if (Password.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
    }
    size += dict_.CalculateSize(_map_dict_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(TestProto other) {
    if (other == null) {
      return;
    }
    if (other.Account.Length != 0) {
      Account = other.Account;
    }
    if (other.Password.Length != 0) {
      Password = other.Password;
    }
    dict_.Add(other.dict_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          Account = input.ReadString();
          break;
        }
        case 18: {
          Password = input.ReadString();
          break;
        }
        case 26: {
          dict_.AddEntriesFrom(input, _map_dict_codec);
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
